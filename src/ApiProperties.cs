using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace restlessmedia.Module.Property.Vebra
{
  [XmlRoot("properties")]
  public class ApiProperties
  {
    [XmlElement("property")]
    public List<ApiProperty> Property { get; set; }

    public int TotalImageCount
    {
      get
      {
        return Property.Sum(x => x.Images.Length);
      }
    }

    public IEnumerable<long> PropertyIds
    {
      get
      {
        return Property.Select(x => x.PropertyId);
      }
    }
  }
}