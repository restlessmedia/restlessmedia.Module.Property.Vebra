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
    public ApiPropertyService(IEmailService emailService, IApiPropertyProvider apiPropertyProvider, IApiPropertyDataProvider apiPropertyDataProvider, IDiskStorageProvider storageProvider, ILicenseSettings licenseSettings, IEmailContext emailContext, ILog log)
    {
      _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
      _apiPropertyProvider = apiPropertyProvider ?? throw new ArgumentNullException(nameof(apiPropertyProvider));
      _apiPropertyDataProvider = apiPropertyDataProvider ?? throw new ArgumentNullException(nameof(apiPropertyDataProvider));
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

      if (sync.IsRunning)
      {
        throw new Exception("Sync is already running");
      }

      try
      {
        review = job.Sync();
      }
      catch (Exception e)
      {
        _log.Exception(e);
        throw;
      }
      finally
      {
        TryEndSync(sync, review, user);
      }

      return review;
    }

    public Task SyncAsync()
    {
      return Task.Run(() => Sync());
    }

    private void TryEndSync(ISync sync, ISyncReview review, IUserInfo user = null)
    {
      sync.IsRunning = false;
      sync.LastAccessed = DateTime.Now;

      try
      {
        if (review != null)
        {
          _emailService.Send(new SyncEmail(_emailContext, review, user));
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

    private readonly IDiskStorageProvider _storageProvider;

    private readonly ILicenseSettings _licenseSettings;

    private readonly IEmailContext _emailContext;

    private readonly ILog _log;
  }
}