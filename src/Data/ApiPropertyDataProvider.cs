using restlessmedia.Module.Data;
using restlessmedia.Module.Property.Data;

namespace restlessmedia.Module.Property.Vebra.Data
{
  internal class ApiPropertyDataProvider : ApiPropertySqlDataProvider, IApiPropertyDataProvider
  {
    public ApiPropertyDataProvider(IDataContext context, IPropertyDataProvider propertyDataProvider, ILog log)
      : base(context, propertyDataProvider, log) { }
  }
}