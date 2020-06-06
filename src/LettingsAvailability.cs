using System.Xml.Serialization;

namespace restlessmedia.Module.Property.Vebra
{
  public enum LettingsAvailability : byte
  {
    [XmlEnum(Name = "1")]
    OnHold = 1,

    [XmlEnum(Name = "2")]
    ToLet = 2,

    [XmlEnum(Name = "3")]
    ReferencesPending = 3,

    [XmlEnum(Name = "4")]
    LetAgreed = 4,

    [XmlEnum(Name = "5")]
    Let = 5,

    [XmlEnum(Name = "6")]
    Withdrawn = 6
  }
}