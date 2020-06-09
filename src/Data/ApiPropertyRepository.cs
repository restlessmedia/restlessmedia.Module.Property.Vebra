using restlessmedia.Module.Data.EF;
using restlessmedia.Module.Property.Data;
using System.Data.Entity;
using System.Linq;

namespace restlessmedia.Module.Property.Vebra.Data
{
  internal class ApiPropertyRepository : Repository<VApiProperty>
  {
    public ApiPropertyRepository(DatabaseContext context)
      : base(context)
    {
      _context = context;
    }

    public VApiProperty Find(long apiPropertyId)
    {
      return Included().SingleOrDefault(x => x.ApiPropertyId == apiPropertyId);
    }

    public VApiProperty Find(int propertyId)
    {
      return Included().SingleOrDefault(x => x.PropertyId == propertyId);
    }

    public VProperty FindWithFiles(long apiPropertyId)
    {
      VApiProperty apiProperty = Find(apiPropertyId);

      if (apiProperty == null)
      {
        return null;
      }

      return apiProperty.Property;
    }

    private IQueryable<VApiProperty> Included()
    {
      return Set()
        .Include(x => x.Property)
        // could we move this into an extension method?
        .Include(x => x.Property.VPropertyFiles)
        .Include(x => x.Property.TBranch)
        .Include(x => x.Property.VDevelopment)
        .Include(x => x.Property.VAddress);
    }

    private readonly DatabaseContext _context;
  }
}