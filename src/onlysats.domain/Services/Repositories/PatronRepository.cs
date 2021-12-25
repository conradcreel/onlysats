using onlysats.domain.Entity;
using onlysats.domain.Models;

namespace onlysats.domain.Services.Repositories;

/// <summary>
/// Encapsulates persistence of Patrons and their Settings
/// </summary>
public interface IPatronRepository
{
    /// <summary>
    /// Convenience method that will execute a performant query to get 
    /// all Patron data including settings. Retrieve only
    /// </summary>
    Task<PatronModel?> GetPatronDetail(int id);

    /// <summary>
    /// Retrieves base Patron entity from its Id
    /// </summary>
    Task<Patron?> GetPatron(int id);

    /// <summary>
    /// Updates or inserts a base Patron entity. On insert, it 
    /// will insert default settings in a single transaction
    /// </summary>
    Task<Patron> UpsertPatron(Patron Patron);

    /// <summary>
    /// Retrieves all of the assets purchased by a Patron and optionally 
    /// filters it by those purchased from a specific Creator
    /// </summary>
    Task<IEnumerable<PatronAsset>> GetPatronAssets(int patronId, int? creatorId = null);
}

#region Implementation

public class PatronRepository : IPatronRepository 
{
    private readonly ISqlRepository _Repository;

    public PatronRepository(ISqlRepository sqlRepository)
    {
        _Repository = sqlRepository;
    }

    public Task<Patron?> GetPatron(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<PatronAsset>> GetPatronAssets(int patronId, int? creatorId = null)
    {
        throw new NotImplementedException();
    }

    public Task<PatronModel?> GetPatronDetail(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Patron> UpsertPatron(Patron Patron)
    {
        throw new NotImplementedException();
    }
}

#endregion 