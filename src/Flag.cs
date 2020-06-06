using System.Xml.Serialization;

namespace restlessmedia.Module.Property.Vebra
{
  public class Flag
  {
    [XmlText]
    public string Value { get; set; }
  }
}