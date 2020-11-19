using restlessmedia.Module.File;
using System;
using System.IO;
using System.Xml.Serialization;

namespace restlessmedia.Module.Property.Vebra
{
  public class Resource : IDownloadable
  {
    public DateTime? Modified
    {
      get
      {
        return DateTime.TryParse(ModifiedValue, out DateTime value) ? value : (DateTime?)null;
      }
    }

    [XmlAttribute("modified")]
    public string ModifiedValue { get; set; }

    [XmlText]
    public string Url { get; set; }

    public Uri Uri
    {
      get
      {
        return Uri.TryCreate(Url, UriKind.RelativeOrAbsolute, out Uri value) ? value : null;
      }
    }

    [XmlIgnore]
    public DownloadStatus Status { get; set; }

    [XmlIgnore]
    public Exception Exception { get; set; }

    [XmlIgnore]
    public virtual FileFlags? Flags
    {
      get
      {
        return null;
      }
    }

    [XmlIgnore]
    public string FileName
    {
      get
      {
        Uri uri = Uri;

        if (uri == null)
        {
          return null;
        }

        return Path.GetFileName(uri.AbsolutePath);
      }
    }

    public string GetCollectionName(ResourceCollection collection)
    {
      if (string.IsNullOrEmpty(FileName))
      {
        return null;
      }

      int index = collection.IndexOf(this);
      return $"{index}_{FileName}";
    }
  }
}