using restlessmedia.Module.Data;
using restlessmedia.Module.Data.EF;
using restlessmedia.Module.Data.Sql;
using restlessmedia.Module.File;
using restlessmedia.Module.File.Data;
using restlessmedia.Module.Property.Data;
using restlessmedia.Module.Property.Data.DataModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;

namespace restlessmedia.Module.Property.Vebra.Data
{
  public class ApiPropertySqlDataProvider : SqlDataProviderBase
  {
    public ApiPropertySqlDataProvider(IDataContext context, IPropertyDataProvider propertyDataProvider, IFileDataProvider fileDataProvider, ILog log)
      : base(context)
    {
      _propertyDataProvider = propertyDataProvider ?? throw new ArgumentNullException(nameof(propertyDataProvider));
      _fileDataProvider = fileDataProvider ?? throw new ArgumentNullException(nameof(fileDataProvider));
      _log = log ?? throw new ArgumentNullException(nameof(log));
    }

    public void SaveSync(ISync sync)
    {
      using (DatabaseContext context = CreateDatabaseContext())
      {
        TApiPropertyState existingState = context.ApiPropertyState.SingleOrDefault(x => x.ApplicationName == DataContext.LicenseSettings.ApplicationName);

        if (existingState == null)
        {
          context.ApiPropertyState.Add(new TApiPropertyState
          {
            LastAccessed = sync.LastAccessed.GetValueOrDefault(DateTime.Now),
            ApplicationName = sync.ApplicationName,
            IsRunning = sync.IsRunning
          });
        }
        else
        {
          existingState.LastAccessed = sync.LastAccessed.GetValueOrDefault(DateTime.Now);
          existingState.ApplicationName = sync.ApplicationName;
          existingState.IsRunning = sync.IsRunning;
        }

        context.SaveChanges();
      }
    }

    public ISync ReadSync()
    {
      using (DatabaseContext context = CreateDatabaseContext())
      {
        TApiPropertyState state = context.ApiPropertyState.SingleOrDefault(x => x.ApplicationName == DataContext.LicenseSettings.ApplicationName);

        if (state == null)
        {
          return null;
        }

        return new ApiPropertySync
        {
          LastAccessed = state.LastAccessed,
          ApplicationName = state.ApplicationName,
          IsRunning = state.IsRunning
        };
      }
    }

    public IProperty Read(long apiPropertyId)
    {
      using (DatabaseContext context = CreateDatabaseContext())
      {
        ApiPropertyRepository apiPropertyRepository = new ApiPropertyRepository(context);
        return apiPropertyRepository.FindWithFiles(apiPropertyId);
      }
    }

    public IEnumerable<string> Delete(int apiPropertyId)
    {
      const string commandName = "dbo.SPDeleteApiProperty";
      return QueryWithTransaction<string>(commandName, new { apiPropertyId });
    }

    public void Reset()
    {
      const string commandName = "dbo.SPApiPropertyReset";
      Execute(commandName);
    }

    public int? GetPropertyEntityId(int apiPropertyId)
    {
      using (DatabaseContext context = CreateDatabaseContext())
      {
        return context.ApiProperty.Where(x => x.ApiPropertyId == apiPropertyId).Select(x => x.PropertyId).SingleOrDefault();
      }
    }

    public void Save(ApiProperty property, ApiPropertyEntity apiPropertyEntity)
    {
      SaveBranch(apiPropertyEntity.Branch);
      _propertyDataProvider.Save(apiPropertyEntity);
      SaveProperty(property, apiPropertyEntity);
    }

    public void SetActive(IEnumerable<long> propertyIds)
    {
      // it's probably more efficient to do this query with sqlText.  the EF alternative would be hacky trying to get the records NOT affected, changing the status properties and saving the results.

      const int limit = 2100;

      if (propertyIds.Count() >= limit)
      {
        throw new InvalidOperationException($"PropertyIds has reached the maximum allowed amount ({limit}) for parameters.");
      }

      // there is a limit to the amount of propertyIds will be parameretized so we might need to add a check here
      const string commandText = "UPDATE TProperty SET Status = @status FROM TProperty P INNER JOIN TAPIProperty API ON API.PropertyEntityId = P.PropertyId WHERE API.PropertyId NOT IN @propertyIds";
      Execute(commandText, new { status = PropertyStatus.Archived, propertyIds = propertyIds }, CommandType.Text);
    }

    public void SaveFile(EntityType entityType, int entityId, FileEntity file)
    {
      using (DatabaseContext context = CreateDatabaseContext())
      {
        FileRepository fileRepository = context.Repository<FileRepository>();
        EntityRepository entityRepository = context.Repository<EntityRepository>();

        File.Data.VEntityFile entityFile = fileRepository.Save(entityType, entityId, file, x =>
        {
          file.FileId = x.FileId;

          if (x.File.SystemFileName != file.SystemFileName)
          {
            _log.Info($"Updating api property file name from {file.SystemFileName} to {x.File.SystemFileName} for id {x.File.FileId}.");
            x.File.SystemFileName = file.SystemFileName;
          }
        });

        context.SaveChanges();

        // update the entity date value for this file
        entityRepository.Update(entityFile.File, file.LastUpdated);
        context.SaveChanges();
      }
    }

    private void SaveProperty(ApiProperty property, ApiPropertyEntity apiPropertyEntity)
    {
      using (DatabaseContext context = CreateDatabaseContext())
      {
        VApiProperty existingProperty = context.ApiProperty.Find(property.PropertyId);

        if (existingProperty == null)
        {
          context.ApiProperty.Add(new VApiProperty
          {
            ApiPropertyId = property.PropertyId,
            PropertyId = apiPropertyEntity.PropertyId.Value,
            LastChangedDate = property.DateLastModified
          });
        }
        else
        {
          existingProperty.LastChangedDate = property.DateLastModified;
        }

        context.SaveChanges();
      }
    }

    private void SaveBranch(IPropertyBranch branch)
    {
      using (DatabaseContext context = CreateDatabaseContext())
      {
        BranchRepository branchRepository = new BranchRepository(context);
        TBranch existingBranch = branchRepository.GetBranch(branch.BranchId, branch.Type);

        if (existingBranch == null)
        {
          existingBranch = context.Branch.Add(new TBranch
          {
            BranchId = branch.BranchId,
            Name = branch.Name,
            Reference = branch.Reference,
            Type = branch.Type
          });
        }
        else
        {
          existingBranch.Name = branch.Name;
          existingBranch.Reference = branch.Reference;
        }

        context.SaveChanges();

        // set the branchGuid back to the calling branch
        branch.BranchGuid = existingBranch.BranchGuid;
      }
    }

    private DatabaseContext CreateDatabaseContext(bool autoDetectChanges = false)
    {
      return new DatabaseContext(DataContext, autoDetectChanges);
    }

    private readonly IPropertyDataProvider _propertyDataProvider;

    private readonly IFileDataProvider _fileDataProvider;

    private readonly ILog _log;
  }
}