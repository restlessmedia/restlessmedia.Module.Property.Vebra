using FakeItEasy;
using restlessmedia.Module.Data;
using restlessmedia.Module.Property.Vebra.Data;
using System;
using Xunit;

namespace restlessmedia.Module.Property.Vebra.Tests
{
  public class ApiPropertyRepositoryTests
  {
    public ApiPropertyRepositoryTests()
    {
      _dataContext = A.Fake<IDataContext>();

      A.CallTo(() => _dataContext.ConnectionFactory).Returns(new IntegrationTestConnetionFactory());

      _context = new DatabaseContext(_dataContext);

      _repository = new ApiPropertyRepository(_context);
    }

    [Fact(Skip = "ignored")]
    public void property_with_includes()
    {
      _repository.FindWithFiles(101617004313);
    }

    private readonly ApiPropertyRepository _repository;

    private readonly DatabaseContext _context;

    private readonly IDataContext _dataContext;

    private readonly Guid _licenseKey = new Guid("BF812A79-9AE3-48F7-8210-FDA1DF411318");
  }
}
