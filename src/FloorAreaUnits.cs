using System.Xml.Serialization;

namespace restlessmedia.Module.Property.Vebra
{
  public enum FloorAreaUnits
  {
    [XmlEnum("")]
    None,

    [XmlEnum("acres")]
    Acres,

    [XmlEnum("hectares")]
    Hectares,

    [XmlEnum("sq m")]
    SqM,

    [XmlEnum("sq ft")]
    SqFt
  }
}