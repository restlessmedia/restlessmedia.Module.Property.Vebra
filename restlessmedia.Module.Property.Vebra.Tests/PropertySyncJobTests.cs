using System;
using System.IO;
using System.Reflection;

namespace restlessmedia.UnitTest.Business.Service.ApiProperty
{
  [TestClass]
  public class PropertySyncJobTests
  {
    public PropertySyncJobTests()
    {
      _diskProvider = A.Fake<IDiskStorageProvider>();
      _job = new PropertySyncJob(A.Fake<IApiPropertyDataProvider>(), _diskProvider, A.Fake<IApiPropertyProvider>());

      //A.CallTo(() => _diskProvider.GetStream()).Returns(GetStream());
    }

    [TestMethod]
    public void Sync_does_stuff()
    {
      _job.Sync();
    }

    [TestMethod]
    public void Sync_does_date()
    {
      const string format = "yyyy-MM-dd-HH_mmss.xml";
      string fileName = DateTime.Now.ToString(format);
    }

    [TestMethod]
    public void xml_serialisation_ampersand()
    {
      ApiProperties properties = new XmlSerializer(typeof(ApiProperties)).Deserialize(GetStream()) as ApiProperties;
    }

    private static Stream GetStream()
    {
      const string resourceName = "restlessmedia.UnitTest.Business.Service.ApiProperty.example_feed.xml";
      return Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
    }

    private readonly PropertySyncJob _job;

    private readonly IDiskStorageProvider _diskProvider;
  }
}