using onlysats.domain.Services.Request.Finder;
using onlysats.domain.Services.Response.Finder;

namespace onlysats.domain.Services;

/// <summary>
/// 
/// </summary>
public interface IFinderService
{
    Task<SearchCreatorResponse> SearchCreators(SearchCreatorRequest request);
}

#region Implementation

public class FinderService : IFinderService
{
    public Task<SearchCreatorResponse> SearchCreators(SearchCreatorRequest request)
    {
        throw new NotImplementedException();
    }
}

#endregion
