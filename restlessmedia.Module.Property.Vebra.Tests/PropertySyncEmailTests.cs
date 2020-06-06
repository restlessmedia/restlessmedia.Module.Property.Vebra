using System.IO;

namespace restlessmedia.UnitTest.Business.Email
{
  [TestClass]
  public class PropertySyncEmailTests
  {
    public PropertySyncEmailTests()
    {
      _data = new XDocument(new XElement("propertieschanged"));
      _apiPropertyProvider = A.Fake<IApiPropertyProvider>();

      SyncContext context = new SyncContext(A.Fake<ISync>(), A.Fake<IServiceContext>(), _apiPropertyProvider, A.Fake<IApiPropertyDataProvider>(), A.Fake<IUserInfo>());

      A.CallTo(() => _apiPropertyProvider.GetProperties()).Returns(_data);

      IConfiguration configuration = A.Fake<IConfiguration>();

      A.CallTo(() => configuration.Email.AdminEmail).Returns("bob@job.com");

      _job = new PropertyChangesSyncJob(context, A.Fake<IFileDownload>());
      _review = new PropertyChangesReview(_job);
      _email = new SyncEmail(configuration, _review, A.Fake<IUserInfo>());
    }

    [TestMethod]
    public void calling_Attachments_returns_XDocument_attachment()
    {
      _job.Sync();

      _email.Attachments.Length.ShouldEqual(1);
    }

    [TestMethod]
    public void calling_Attachments_streams_XDocument_attachment()
    {
      _job.Sync();

      using (Stream stream = _email.Attachments[0].ContentStream)
      {
        StreamReader reader = new StreamReader(stream);
        string xml = reader.ReadToEnd();
        XNode.DeepEquals(XDocument.Parse(xml), _data).ShouldBeTrue();
      }
    }

    private readonly SyncEmail _email;

    private readonly PropertyChangesSyncJob _job;

    private readonly PropertyChangesReview _review;

    private readonly IApiPropertyProvider _apiPropertyProvider;

    private readonly XDocument _data;
  }
}