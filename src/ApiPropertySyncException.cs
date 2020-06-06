using restlessmedia.Module.Email;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;

namespace restlessmedia.Module.Property.Vebra
{
  public class ApiPropertySyncException : Exception
  {
    public ApiPropertySyncException(XDocument xml, string message, Exception innerException)
      : this(message, innerException)
    {
      Xml = xml;
    }

    public ApiPropertySyncException(string message, Exception innerException)
      : base(message, innerException) { }

    public ApiPropertySyncException(string message)
      : base(message) { }

    public override IDictionary Data
    {
      get
      {
        return new Dictionary<string, object>
        {
          { "Xml", new XDocumentAttachment("data", Xml) }
        };
      }
    }

    public readonly XDocument Xml;
  }
}