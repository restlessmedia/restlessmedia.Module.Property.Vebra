using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using System.Configuration;

namespace restlessmedia.Module.Property.Vebra.Configuration
{
  public class ApiPropertySettings : SerializableConfigurationSection, IApiPropertySettings
  {
    [ConfigurationProperty(_urlProperty, IsRequired = true)]
    public string Url
    {
      get
      {
        return (string)this[_urlProperty];
      }
    }

    [ConfigurationProperty(_timeoutProperty, IsRequired = false, DefaultValue = 30)]
    public int Timeout
    {
      get
      {
        return (int)this[_timeoutProperty];
      }
    }

    private const string _urlProperty = "url";

    private const string _timeoutProperty = "timeout";
  }
}