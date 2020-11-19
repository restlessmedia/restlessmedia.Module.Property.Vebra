using FakeItEasy;
using restlessmedia.Module.File;
using restlessmedia.Module.Property.Vebra.Data;
using System;
using System.IO;
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

      A.CallTo(() => _apiPropertyDataProvider.Read(A<long>.Ignored))
        .ReturnsLazily(() =>
        {
          IProperty property = A.Fake<IProperty>();
          property.PropertyId = 123;
          return property;
        });

      DownloadHelper.WebClientFactory = () => A.Fake<IWebClient>();
    }

    [Theory]
    [ResourceInlineData("restlessmedia.Module.Property.Vebra.Tests.example_feed.xml")]
    public void xml_serialisation_ampersand(Stream stream)
    {
      A.CallTo(() => _apiPropertyProvider.GetStream()).Returns(stream);
      ApiProperties properties = new XmlSerializer(typeof(ApiProperties)).Deserialize(stream) as ApiProperties;
    }

    private readonly PropertySyncJob _job;

    private readonly IDiskStorageProvider _diskProvider;

    private readonly IApiPropertyDataProvider _apiPropertyDataProvider;

    private readonly IApiPropertyProvider _apiPropertyProvider;
  }
}