using System.Xml.Serialization;

namespace restlessmedia.Module.Property.Vebra
{
  public enum CommercialAvailability : byte
  {
	  [XmlEnum(Name = "0")]
    None = 0, // also equal to onhold
	  
    [XmlEnum(Name = "1")]
    OnHold = 1,

    [XmlEnum(Name = "2")]
    ForSale = 2,

    [XmlEnum(Name = "3")]
    ToLet = 3,

    [XmlEnum(Name = "4")]
    ForSaleOrToLet = 4,

    [XmlEnum(Name = "5")]
    UnderOffer = 5,

    [XmlEnum(Name = "6")]
    SoldSTC = 6,

    [XmlEnum(Name = "7")]
    Exchanged = 7,

    [XmlEnum(Name = "8")]
    Completed = 8,

    [XmlEnum(Name = "9")]
    LetAgreed = 9,

    [XmlEnum(Name = "10")]
    Let = 10,

    [XmlEnum(Name = "11")]
    Withdrawn = 11
  }
}