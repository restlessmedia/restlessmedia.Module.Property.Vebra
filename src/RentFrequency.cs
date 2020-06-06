using System.Xml.Serialization;

namespace restlessmedia.Module.Property.Vebra
{
  public enum RentFrequency
  {
    None = 0,

    [XmlEnum("1")]
    pcm = 1,

    [XmlEnum("2")]
    pw = 2,

    [XmlEnum("3")]
    pa = 3
  }
}