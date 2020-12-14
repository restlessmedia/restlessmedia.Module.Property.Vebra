using FakeItEasy;
using restlessmedia.Module.Data;
using restlessmedia.Module.File;
using restlessmedia.Module.File.Data;
using restlessmedia.Module.Property.Data;
using restlessmedia.Module.Property.Vebra.Data;
using System.Data;
using System.Data.Common;
using Xunit;

namespace restlessmedia.Module.Property.Vebra.Tests.Data
{
  public class ApiPropertySqlDataProviderTests
  {
    public ApiPropertySqlDataProviderTests()
    {
      IDataContext dataContext = A.Fake<IDataContext>();
      IPropertyDataProvider propertyDataProvider = A.Fake<IPropertyDataProvider>();
      IFileDataProvider fileDataProvider = A.Fake<IFileDataProvider>();
      ILog log = A.Fake<ILog>();
      IDbConnection dbConnection = A.Fake<DbConnection>();

      A.CallTo(() => dataContext.ConnectionFactory.CreateConnection(A<bool>.Ignored)).Returns(dbConnection);

      _apiPropertySqlDataProvider = new ApiPropertySqlDataProvider(dataContext, propertyDataProvider, log);
    }

    [Fact]
    public void asd()
    {
      _apiPropertySqlDataProvider.SaveFile(EntityType.Property, 1, new FileEntity());
    }

    private readonly ApiPropertySqlDataProvider _apiPropertySqlDataProvider;
  }
}