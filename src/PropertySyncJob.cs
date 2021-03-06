﻿using restlessmedia.Module.File;
using restlessmedia.Module.Property.Vebra.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace restlessmedia.Module.Property.Vebra
{
  public class PropertySyncJob
  {
    public PropertySyncJob(IApiPropertyDataProvider apiPropertyDataProvider, IDiskStorageProvider storageProvider, IApiPropertyProvider apiPropertyProvider, ILog log)
    {
      _apiPropertyDataProvider = apiPropertyDataProvider ?? throw new ArgumentNullException(nameof(apiPropertyDataProvider));
      _storageProvider = storageProvider ?? throw new ArgumentNullException(nameof(storageProvider));
      _apiPropertyProvider = apiPropertyProvider ?? throw new ArgumentNullException(nameof(apiPropertyProvider));
      _review = new PropertySyncReview();
      _log = log ?? throw new ArgumentNullException(nameof(log));
    }

    public ISyncReview Sync()
    {
      _log.Info("Property sync start");
      string xml = GetXml();
      SaveCopy(xml);
      ApiProperties properties = SyncPropertyXml(xml);
      _review.Properties = properties;
      _apiPropertyDataProvider.SetActive(properties.PropertyIds);
      _log.Info("Property sync complete");
      return _review;
    }

    private string GetXml()
    {
      using (StreamReader reader = new StreamReader(_apiPropertyProvider.GetStream()))
      {
        return reader.ReadToEnd();
      }
    }

    private ApiProperties SyncPropertyXml(string xml)
    {
      ApiProperties properties = DeserializeXml(xml);

      if (properties != null)
      {
        // sync each property
        foreach (ApiProperty property in properties.Property)
        {
          try
          {
            _review.Messages.Add($"Syncing property {property.DisplayAddress} ({property.Images.Count()} images)");
            SyncProperty(property);
          }
          catch (Exception e)
          {
            _review.Exceptions.Add(e);
            _log.Exception(e, $"{nameof(SyncPropertyXml)} ({property.DisplayAddress})");
          }
        }
      }

      return properties;
    }

    private void SaveCopy(string xml)
    {
      const string format = "yyyy-MM-dd_HHmmss";
      const string extension = ".xml";
      string fileName = string.Concat(DateTime.Now.ToString(format), extension);
      _storageProvider.Put("propertysource", fileName, xml);
    }

    private void SyncProperty(ApiProperty property)
    {
      IProperty propertyEntity = _apiPropertyDataProvider.Read(property.PropertyId);
      ApiPropertyEntity apiPropertyEntity = new ApiPropertyEntity(property, propertyEntity);
      _apiPropertyDataProvider.Save(property, apiPropertyEntity);
      SyncResources(property, propertyEntity, apiPropertyEntity);
    }

    private void SyncResources(ApiProperty property, IProperty propertyEntity, ApiPropertyEntity apiPropertyEntity)
    {
      IEnumerable<Resource> resources = property.Images
        .Union(property.Floorplans)
        .Union(property.EpcGraphs)
        .Where(x => ShouldUpdate(x, propertyEntity));
      ResourceCollection resourceCollection = new ResourceCollection(resources);
      DownloadResources(resourceCollection);
      SaveResources(resourceCollection, apiPropertyEntity);
    }

    private bool ShouldUpdate(Resource resource, IProperty propertyEntity)
    {
      if (propertyEntity == null || !resource.Modified.HasValue || propertyEntity.Files == null)
      {
        return true;
      }

      IFile existingFile = propertyEntity.Files.FirstOrDefault(x => string.Equals(x.FileName, resource.FileName));

      if (existingFile == null || !existingFile.LastUpdated.HasValue)
      {
        return true;
      }

      return DateTime.Compare(existingFile.LastUpdated.Value, resource.Modified.Value) < 0;
    }

    private void SaveResources(ResourceCollection resources, ApiPropertyEntity apiPropertyEntity)
    {
      foreach (Resource resource in resources)
      {
        if (resource.Status == DownloadStatus.Success)
        {
          try
          {
            _apiPropertyDataProvider.SaveFile(EntityType.Property, apiPropertyEntity.PropertyId.Value, new ApiFileEntity(resources, resource));
            _log.Info($"Saving resource {resource.FileName} for property {apiPropertyEntity.Title}.");
          }
          catch (Exception e)
          {
            _log.Exception(e);
            _review.Exceptions.Add(e);
          }
        }
        else
        {
          _log.Warning($"Resource {resource.Uri} returned a status of {resource.Status} for {apiPropertyEntity.Title}, resource will not be saved.");
        }
      }
    }

    private void DownloadResources(ResourceCollection resources)
    {
      const string path = "property";

      foreach (Resource resource in resources)
      {
        // deletes the old non-ordered images
        _storageProvider.DeleteIfExists(path, resource.FileName);

        // this will register a resource with a name prefixed by the index it appears in the collection
        // this is because the name of the image is not as reliable as the order in which it is supplied
        if (!DownloadHelper.TryDownload(resource, _storageProvider, path, resource.GetCollectionName(resources)))
        {
          _log.Warning($"Resource {resource.Uri} failed to download. {resource.Exception ?? null}");
        }
      }
    }

    private static ApiProperties DeserializeXml(string xml)
    {
      if (string.IsNullOrWhiteSpace(xml))
      {
        throw new ApiPropertySyncException("There was no xml to deserialise");
      }

      using (TextReader reader = new StringReader(xml))
      {
        // info: this ctor caches the type mapping so should be quicker after the first initialisation
        return new XmlSerializer(typeof(ApiProperties)).Deserialize(reader) as ApiProperties;
      }
    }

    private readonly PropertySyncReview _review;

    private readonly IApiPropertyDataProvider _apiPropertyDataProvider;

    private readonly IDiskStorageProvider _storageProvider;

    private readonly IApiPropertyProvider _apiPropertyProvider;

    private readonly ILog _log;
  }
}