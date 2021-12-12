using onlysats.domain.Models;

namespace onlysats.domain.Services.Repositories;

/// <summary>
/// Encapsulates persistence of Creators and their Settings
/// </summary>
public interface ICreatorRepository
{

}

#region Implementation

public class CreatorRepository : ICreatorRepository 
{
    private readonly ISqlRepository _Repository;

    public CreatorRepository(ISqlRepository sqlRepository)
    {
        _Repository = sqlRepository;
    }
}

#endregion 