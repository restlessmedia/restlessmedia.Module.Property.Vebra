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
      _webClient = A.Fake<IWebClient>();

      A.CallTo(() => _apiPropertyDataProvider.Read(A<long>.Ignored))
        .ReturnsLazily(() =>
        {
          IProperty property = A.Fake<IProperty>();
          property.PropertyId = 123;
          return property;
        });

      DownloadHelper.WebClientFactory = () => _webClient;
    }

    [Theory]
    [ResourceInlineData("restlessmedia.Module.Property.Vebra.Tests.example_feed.xml")]
    public void xml_serialisation_ampersand(Stream stream)
    {
      A.CallTo(() => _apiPropertyProvider.GetStream()).Returns(stream);
      ApiProperties properties = new XmlSerializer(typeof(ApiProperties)).Deserialize(stream) as ApiProperties;
    }

    [Theory]
    [ResourceInlineData("restlessmedia.Module.Property.Vebra.Tests.example_feed.xml")]
    public void saves_resources(Stream stream)
    {
      // set-up
      A.CallTo(() => _apiPropertyProvider.GetStream()).Returns(stream);

      // call
      _job.Sync();

      // assert
      AssertFileSaved(file => file.FileName == "IMG_9351_1_large.jpg");
      AssertFileSaved(file => file.FileName == "IMG_9351_2_large.jpg");
      AssertFileSaved(file => file.FileName == "FLP_413_1_large.png");
      AssertFileSaved(file => file.FileName == "MED_413_2246.jpg");
    }

    [Theory]
    [ResourceInlineData("restlessmedia.Module.Property.Vebra.Tests.example_feed.xml")]
    public void downloads_resources(Stream stream)
    {
      // set-up
      A.CallTo(() => _apiPropertyProvider.GetStream()).Returns(stream);

      // call
      _job.Sync();

      // assert
      AssertFileDownloaded(uri => uri.ToString() == "http://media2.jupix.co.uk/v3/clients/1617/properties/9351/IMG_9351_1_large.jpg");
    }

    private void AssertFileSaved(Func<FileEntity, bool> factory)
    {
      A.CallTo(() => _apiPropertyDataProvider.SaveFile(EntityType.Property, A<int>.Ignored, A<FileEntity>.Ignored))
       .WhenArgumentsMatch(args => factory(args.Get<FileEntity>(2)))
       .MustHaveHappened();
    }

    private void AssertFileDownloaded(Func<Uri, bool> factory)
    {
      A.CallTo(() => _webClient.OpenRead(A<Uri>.Ignored))
        .WhenArgumentsMatch(args => factory(args.Get<Uri>(0)))
        .MustHaveHappened();
    }

    private readonly PropertySyncJob _job;

    private readonly IDiskStorageProvider _diskProvider;

    private readonly IApiPropertyDataProvider _apiPropertyDataProvider;

    private readonly IApiPropertyProvider _apiPropertyProvider;

    private readonly IWebClient _webClient;
  }
}