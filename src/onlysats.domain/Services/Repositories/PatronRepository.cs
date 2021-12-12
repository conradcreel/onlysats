using onlysats.domain.Models;

namespace onlysats.domain.Services.Repositories;

/// <summary>
/// Encapsulates persistence of Patrons and their Settings
/// </summary>
public interface IPatronRepository
{

}

#region Implementation

public class PatronRepository : IPatronRepository 
{
    private readonly ISqlRepository _Repository;

    public PatronRepository(ISqlRepository sqlRepository)
    {
        _Repository = sqlRepository;
    }
}

#endregion 