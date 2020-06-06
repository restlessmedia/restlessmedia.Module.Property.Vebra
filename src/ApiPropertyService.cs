using restlessmedia.Module.Configuration;
using restlessmedia.Module.Email;
using restlessmedia.Module.File;
using restlessmedia.Module.Property.Vebra.Data;
using restlessmedia.Module.Security;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace restlessmedia.Module.Property.Vebra
{
  internal sealed class ApiPropertyService : IApiPropertyService
  {
    public ApiPropertyService(IEmailService emailService, IApiPropertyProvider apiPropertyProvider, IApiPropertyDataProvider apiPropertyDataProvider, IFileService fileService, IDiskStorageProvider storageProvider, ILicenseSettings licenseSettings, IEmailContext emailContext, ILog log)
    {
      _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
      _apiPropertyProvider = apiPropertyProvider ?? throw new ArgumentNullException(nameof(apiPropertyProvider));
      _apiPropertyDataProvider = apiPropertyDataProvider ?? throw new ArgumentNullException(nameof(apiPropertyDataProvider));
      _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
      _storageProvider = storageProvider ?? throw new ArgumentNullException(nameof(storageProvider));
      _licenseSettings = licenseSettings ?? throw new ArgumentNullException(nameof(licenseSettings));
      _emailContext = emailContext ?? throw new ArgumentNullException(nameof(emailContext));
      _log = log ?? throw new ArgumentNullException(nameof(log));
    }

    public void Delete(int propertyId)
    {
      IEnumerable<string> orphanFiles = _apiPropertyDataProvider.Delete(propertyId);

      foreach (string file in orphanFiles)
      {
        _storageProvider.DeleteIfExists("apiProperty", file);
      }
    }

    public IProperty Read(long apiPropertyId)
    {
      return _apiPropertyDataProvider.Read(apiPropertyId);
    }

    public IProperty Read(int propertyId)
    {
      return _apiPropertyDataProvider.Read(propertyId);
    }

    public ISync ReadSync()
    {
      return _apiPropertyDataProvider.ReadSync();
    }

    public int? GetPropertyEntityId(int propertyId)
    {
      return _apiPropertyDataProvider.GetPropertyEntityId(propertyId);
    }

    public ISyncReview Sync(IUserInfo user = null)
    {
      ISync sync = ReadSync() ?? new ApiPropertySync()
      {
        ApplicationName = _licenseSettings.ApplicationName,
        LastAccessed = DateTime.Now
      };

      PropertySyncJob job = new PropertySyncJob(_apiPropertyDataProvider, _storageProvider, _apiPropertyProvider, _log);
      ISyncReview review = null;

      try
      {
        review = Sync(sync, job);
      }
      catch (Exception e)
      {
        _log.Exception(e);
        throw;
      }
      finally
      {
        try
        {
          EndSync(sync, review, job, user);
        }
        catch (Exception e)
        {
          _log.Exception(e);
          throw;
        }
      }

      return review;
    }

    public void SyncAsync()
    {
      Task.Run(() => Sync(null));
    }

    /// <summary>
    /// Performs a sync for the given sync type
    /// </summary>
    /// <param name="sync"></param>
    private ISyncReview Sync(ISync sync, PropertySyncJob job)
    {
      if (sync.IsRunning)
      {
        throw new Exception("Sync is already running");
      }

      return job.Sync();
    }

    private void Notify(ISyncReview review, IUserInfo user = null)
    {
      _emailService.Send(new SyncEmail(_emailContext, review, user));
    }

    private void EndSync(ISync sync, ISyncReview review, PropertySyncJob job, IUserInfo user = null)
    {
      sync.IsRunning = false;
      sync.LastAccessed = DateTime.Now;

      try
      {
        if (review != null)
        {
          Notify(review, user);
        }
      }
      catch (Exception e)
      {
        _log.Exception(e);
      }
      finally
      {
        _apiPropertyDataProvider.SaveSync(sync);
      }
    }

    private readonly IApiPropertyProvider _apiPropertyProvider;

    private readonly IEmailService _emailService;

    private readonly IApiPropertyDataProvider _apiPropertyDataProvider;

    private readonly IFileService _fileService;

    private readonly IDiskStorageProvider _storageProvider;

    private readonly ILicenseSettings _licenseSettings;

    private readonly IEmailContext _emailContext;

    private readonly ILog _log;
  }
}