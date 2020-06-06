using restlessmedia.Module.Property.Vebra.Configuration;
using System;
using System.IO;
using System.Net;

namespace restlessmedia.Module.Property.Vebra
{
  public class ApiPropertyProvider : IApiPropertyProvider
  {
    public ApiPropertyProvider(IApiPropertySettings settings, ILog log)
    {
      _apiPropertySettings = settings;
      _log = log;
    }

    public Stream GetStream()
    {
      WebResponse response = GetResponse(_apiPropertySettings.Url);
      return response.GetResponseStream();
    }

    /// <summary>
    /// Performs neccessary routines to provide a valid response for the url
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    private WebResponse GetResponse(string url)
    {
      if (string.IsNullOrEmpty(url))
      {
        throw new ArgumentNullException(nameof(url));
      }

      HttpWebRequest request = WebRequest.CreateHttp(url);

      try
      {
        return request.GetResponse();
      }
      catch (Exception e)
      {
        _log.Exception(e);
        throw;
      }
    }

    private readonly IApiPropertySettings _apiPropertySettings;

    private readonly ILog _log;
  }
}