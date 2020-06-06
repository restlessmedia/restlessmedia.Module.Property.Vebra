using restlessmedia.Module.Security;

namespace restlessmedia.Module.Property.Vebra
{
  public interface IApiPropertyService : IService
  {
    /// <summary>
    /// Deletes a property by id
    /// </summary>
    /// <param name="propertyId"></param>
    void Delete(int propertyId);

    /// <summary>
    /// This will return a property entity for the given propertyid - if the record does not exist, it returns null
    /// </summary>
    /// <param name="apiPropertyId"></param>
    /// <returns></returns>
    IProperty Read(long apiPropertyId);

    /// <summary>
    /// This will return a property entity for the given propertyid - if the record does not exist, it returns null
    /// </summary>
    /// <param name="propertyId"></param>
    /// <returns></returns>
    IProperty Read(int propertyId);

    /// <summary>
    /// Returns a sync object of the given type
    /// </summary>
    /// <returns></returns>
    ISync ReadSync();

    /// <summary>
    /// Returns the internal property entity id for the given api property id
    /// </summary>
    /// <param name="propertyId"></param>
    /// <returns></returns>
    int? GetPropertyEntityId(int propertyId);

    /// <summary>
    /// Performs a sync of the specified type
    /// </summary>
    /// <param name="user"></param>
    ISyncReview Sync(IUserInfo user = null);

    void SyncAsync();
  }
}