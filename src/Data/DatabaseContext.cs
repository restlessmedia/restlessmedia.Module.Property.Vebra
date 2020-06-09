using restlessmedia.Module.Data;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace restlessmedia.Module.Property.Vebra.Data
{
  internal class DatabaseContext : Property.Data.DatabaseContext
  {
    public DatabaseContext(IDataContext dataContext, bool autoDetectChanges = false)
      : base(dataContext, autoDetectChanges)
    {
      _dataContext = dataContext;
    }

    public DbSet<VApiProperty> ApiProperty
    {
      get
      {
        return Set<VApiProperty>();
      }
    }

    public DbSet<TApiPropertyState> ApiPropertyState
    {
      get
      {
        return Set<TApiPropertyState>();
      }
    }

    protected override void Configure(DbModelBuilder modelBuilder)
    {
      base.Configure(modelBuilder);

      modelBuilder.Configurations.Add(new EntityTypeConfiguration<VApiProperty>());
    }

    private readonly IDataContext _dataContext;
  }
}