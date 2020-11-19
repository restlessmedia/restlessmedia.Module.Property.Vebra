using FakeItEasy;
using restlessmedia.Module.Configuration;
using restlessmedia.Module.Email;
using restlessmedia.Module.File;
using restlessmedia.Module.Property.Vebra.Data;
using System.IO;
using Xunit;

namespace restlessmedia.Module.Property.Vebra.Tests
{
  public class ApiPropertyServiceTests
  {
    public ApiPropertyServiceTests()
    {
      _apiPropertyProvider = A.Fake<IApiPropertyProvider>();
      _apiPropertyDataProvider = A.Fake<IApiPropertyDataProvider>();
      _emailContext = A.Fake<IEmailContext>();
      _apiPropertyService = new ApiPropertyService(A.Fake< IEmailService>(), _apiPropertyProvider, _apiPropertyDataProvider, A.Fake<IDiskStorageProvider>(), A.Fake<ILicenseSettings>(), _emailContext, A.Fake<ILog>());

      // we send the emails to admin and the sync email routines expect this to be set
      A.CallTo(() => _emailContext.EmailSettings.AdminEmail).Returns("test@site.com");
    }

    /// <summary>
    /// Ensure the sync always gets saved which ensures its state (isRunning etc) is updated
    /// </summary>
    /// <param name="xml"></param>
    [Theory]
    [InlineData("<?xml version=\"1.0\" encoding=\"iso-8859-1\" ?><properties />")]
    // this will force an error as there is no xml data
    [InlineData(null)]
    public void sync_always_saves(string xml)
    {
      // set-up
      SetResponseXml(xml);

      // call
      _apiPropertyService.Sync();

      // assert
      A.CallTo(() => _apiPropertyDataProvider.SaveSync(A<ISync>.Ignored))
        .WhenArgumentsMatch(x => !x.Get<ISync>(0).IsRunning)
        .MustHaveHappenedOnceExactly();
    }

    private void SetResponseXml(string xml)
    {
      MemoryStream memoryStream = new MemoryStream();
      StreamWriter streamWriter = new StreamWriter(memoryStream);
      streamWriter.Write(xml);
      streamWriter.Flush();
      memoryStream.Seek(0, SeekOrigin.Begin);
      A.CallTo(() => _apiPropertyProvider.GetStream()).Returns(memoryStream);
    }

    private readonly ApiPropertyService _apiPropertyService;

    private readonly IApiPropertyProvider _apiPropertyProvider;

    private readonly IApiPropertyDataProvider _apiPropertyDataProvider;

    private readonly IEmailContext _emailContext; 
  }
}
