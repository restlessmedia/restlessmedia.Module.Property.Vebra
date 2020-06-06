namespace restlessmedia.UnitTest.Data.Provider.Sql
{
  [TestClass]
  public class ApiPropertySqlDataProviderTests
  {
    public ApiPropertySqlDataProviderTests()
    {
      _context = A.Fake<IDataContext>();
      //_provider = new VebraSqlDataProvider(_context);
    }

    [TestMethod]
    public void SyncProperties_uses_timeout()
    {
      //_provider.SyncProperties(new System.Xml.Linq.XDocument());

      //A.CallTo(_context.Configuration.ApiProperty).Where(x => x.Method.Name == "get_Timeout").MustHaveHappened();
    }

    //private readonly VebraSqlDataProvider _provider;

    private readonly IDataContext _context;
  }
}
