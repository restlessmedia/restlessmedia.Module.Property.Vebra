using restlessmedia.Module.File;
using System;

namespace restlessmedia.Module.Property.Vebra
{
  public class ApiFileEntity : FileEntity
  {
    public ApiFileEntity(ResourceCollection collection, Resource resource)
    {
      _collection = collection ?? throw new ArgumentNullException(nameof(collection));
      _resource = resource ?? throw new ArgumentNullException(nameof(resource));
    }

    public override string FileName
    {
      get
      {
        return _resource.FileName;
      }
    }

    public override string SystemFileName
    {
      get
      {
        return _resource.GetCollectionName(_collection);
      }
    }

    public override FileType FileType
    {
      get
      {
        return FileType.Image;
      }
    }

    public override int? Flags
    {
      get
      {
        return (int?)_resource.Flags;
      }
    }

    public override DateTime? LastUpdated
    {
      get
      {
        return _resource.Modified;
      }
    }

    private readonly ResourceCollection _collection;

    private readonly Resource _resource;
  }
}