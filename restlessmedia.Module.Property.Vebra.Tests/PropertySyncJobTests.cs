using FakeItEasy;
using restlessmedia.Module.File;
using restlessmedia.Module.Property.Vebra.Data;
using System;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using Xunit;

namespace restlessmedia.Module.Property.Vebra.Tests
{
  public class PropertySyncJobTests
  {
    public PropertySyncJobTests()
    {
      _apiPropertyDataProvider = A.Fake<IApiPropertyDataProvider>();
      _diskProvider = A.Fake<IDiskStorageProvider>();
      _apiPropertyProvider = A.Fake<IApiPropertyProvider>();
      _job = new PropertySyncJob(_apiPropertyDataProvider, _diskProvider, _apiPropertyProvider, A.Fake<ILog>());

      A.CallTo(() => _apiPropertyProvider.GetStream()).Returns(GetStream());
      A.CallTo(() => _apiPropertyDataProvider.Read(A<long>.Ignored))
        .ReturnsLazily(() =>
        {
          IProperty property = A.Fake<IProperty>();
          property.PropertyId = 123;
          return property;
        });

      DownloadHelper.WebClientFactory = () => A.Fake<IWebClient>();
    }

    [Fact]
    public void Sync_does_stuff()
    {
      _job.Sync();
    }

    [Fact]
    public void Sync_does_date()
    {
      const string format = "yyyy-MM-dd-HH_mmss.xml";
      string fileName = DateTime.Now.ToString(format);
    }

    [Fact]
    public void xml_serialisation_ampersand()
    {
      ApiProperties properties = new XmlSerializer(typeof(ApiProperties)).Deserialize(GetStream()) as ApiProperties;
    }

    private static Stream GetStream()
    {
      const string resourceName = "restlessmedia.Module.Property.Vebra.Tests.example_feed.xml";
      return Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
    }

    private readonly PropertySyncJob _job;

    private readonly IDiskStorageProvider _diskProvider;

    private readonly IApiPropertyDataProvider _apiPropertyDataProvider;

    private readonly IApiPropertyProvider _apiPropertyProvider;
  }
}