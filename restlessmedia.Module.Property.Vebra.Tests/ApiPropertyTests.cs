using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using Xunit;

namespace restlessmedia.Module.Property.Vebra.Tests
{
  public class ApiPropertyTests
  {
    public ApiPropertyTests()
    {

    }

    [Fact]
    public void test_load_xml()
    {
      ApiProperties properties = _serializer.Deserialize(GetResourceStream()) as ApiProperties;
    }

    [Fact]
    public void TotalFileCount_returns_total_of_all_resources()
    {
      ApiProperties properties = _serializer.Deserialize(GetResourceStream()) as ApiProperties;

      int s = properties.TotalImageCount;
    }

    [Fact]
    public void sadasd()
    {
      string xml = "<test><images><image src=\"foo\"/><image src=\"foo\"/></images></test>";
      StringReader reader = new StringReader(xml);
      
      var t = new XmlSerializer(typeof(test)).Deserialize(reader) as test;
    }

    public class test
    {
      public ResourceCollection<Image> Images { get; set; }
    }

    public class ResourceCollection<T>
    {

    }

    public class Image
    {

    }

    private static Stream GetResourceStream()
    {
      const string resource = "restlessmedia.Module.Property.Vebra.Tests.example_feed.xml";
      return Assembly.GetExecutingAssembly().GetManifestResourceStream(resource);
    }

    private readonly static XmlSerializer _serializer = new XmlSerializer(typeof(ApiProperties));
  }
}