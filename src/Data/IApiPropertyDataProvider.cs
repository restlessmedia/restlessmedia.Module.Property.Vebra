using restlessmedia.Module.Data;
using restlessmedia.Module.File;
using System.Collections.Generic;

namespace restlessmedia.Module.Property.Vebra.Data
{
  public interface IApiPropertyDataProvider : IDataProvider
  {
    void SaveSync(ISync sync);

    ISync ReadSync();

    /// <summary>
    /// Reads a property based on the api property id
    /// </summary>
    /// <param name="apiPropertyId"></param>
    /// <returns></returns>
    IProperty Read(long apiPropertyId);

    /// <summary>
    /// Deletes and returns any file references it will orphan
    /// </summary>
    /// <param name="propertyId"></param>
    /// <returns></returns>
    IEnumerable<string> Delete(int propertyId);

    void Reset();

    int? GetPropertyEntityId(int propertyId);

    /// <summary>
    /// Sets the properties for the given ids as active.  All other properties are archived.
    /// </summary>
    /// <param name="propertyIds"></param>
    void SetActive(IEnumerable<long> propertyIds);

    void Save(ApiProperty property, ApiPropertyEntity apiPropertyEntity);

    void SaveFile(EntityType entityType, int entityId, FileEntity file);
  }
}