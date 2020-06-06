using System;
using System.Xml.Serialization;

namespace restlessmedia.Module.Property.Vebra
{
  public class ExternalLink
  {
    [XmlElement("url")]
    public string Url { get; set; }

    [XmlElement("description")]
    public string Description { get; set; }

    [XmlElement("modified", type: typeof(DateTime))]
    public DateTime Modified { get; set; }
  }
}