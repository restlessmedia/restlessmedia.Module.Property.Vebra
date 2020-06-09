using restlessmedia.Module.File;
using restlessmedia.Module.Property.Vebra.Data;
using restlessmedia.Module.Security;
using System;

namespace restlessmedia.Module.Property.Vebra
{
  internal class SyncContext
  {
    public SyncContext(ISync sync, IDiskStorageProvider storageProvider, IApiPropertyProvider provider, IApiPropertyDataProvider dataProvider, IFileService fileService, IUserInfo user = null)
    {
      Sync = sync ?? throw new ArgumentNullException(nameof(sync));
      StorageProvider = storageProvider ?? throw new ArgumentNullException(nameof(storageProvider));
      Provider = provider ?? throw new ArgumentNullException(nameof(provider));
      DataProvider = dataProvider ?? throw new ArgumentNullException(nameof(dataProvider));
      FileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
      User = user;
    }

    public ISync Sync { get; private set; }

    public IDiskStorageProvider StorageProvider { get; private set; }

    public IApiPropertyProvider Provider { get; private set; }

    public IApiPropertyDataProvider DataProvider { get; private set; }

    public IFileService FileService { get; private set; }

    public IUserInfo User { get; private set; }
  }
}