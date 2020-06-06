using System.IO;
using System.Reflection;

namespace restlessmedia.UnitTest.Abstractions.Provider.ApiProperty
{
  [TestClass]
  public class ApiPropertyTests
  {
    public ApiPropertyTests()
    {

    }

    [TestMethod]
    public void test_load_xml()
    {
      ApiProperties properties = _serializer.Deserialize(GetResourceStream()) as ApiProperties;
    }

    [TestMethod]
    public void TotalFileCount_returns_total_of_all_resources()
    {
      ApiProperties properties = _serializer.Deserialize(GetResourceStream()) as ApiProperties;

      int s = properties.TotalImageCount;
    }

    [TestMethod]
    public void sadasd()
    {
      string xml = "<test><images><image src=\"foo\"/><image src=\"foo\"/></image></test>";
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
      const string resource = "restlessmedia.UnitTest.Abstractions.Provider.ApiProperty.example_feed.xml";
      return Assembly.GetExecutingAssembly().GetManifestResourceStream(resource);
    }

    private readonly static XmlSerializer _serializer = new XmlSerializer(typeof(ApiProperties));
  }
}