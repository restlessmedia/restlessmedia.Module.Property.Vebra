using System.Xml.Serialization;

namespace restlessmedia.Module.Property.Vebra
{
  public enum SalesAvailability : byte
  {
    [XmlEnum(Name = "1")]
    OnHold = 1,

    [XmlEnum(Name = "2")]
    ForSale = 2,

    [XmlEnum(Name = "3")]
    UnderOffer = 3,

    [XmlEnum(Name = "4")]
    SoldSTC = 4,

    [XmlEnum(Name = "5")]
    Sold = 5,

    [XmlEnum(Name = "7")]
    Withdrawn = 7,
  }
}