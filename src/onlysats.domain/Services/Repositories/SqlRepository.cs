using onlysats.domain.Models;

namespace onlysats.domain.Services.Repositories;

/// <summary>
/// Encapsulates the SQL flavor implementation we choose so that the 
/// domain repositories can remain (mostly) ignorant. Once Dapr query 
/// stores go into at least Beta, this can be refactored to use Dapr.
/// </summary>
public interface ISqlRepository
{
    Task<T> SelectSingle<T>(string sql);
    Task<IEnumerable<T>> SelectMultiple<T>(string sql);
    Task<T> Upsert<T>(string sql);
}


public class SqlRepository : ISqlRepository
{
    public SqlRepository(OnlySatsConfiguration config)
    {
        // TODO: Rest of Driver-specific setup    
    }

    public Task<IEnumerable<T>> SelectMultiple<T>(string sql)
    {
        throw new NotImplementedException();
    }

    public Task<T> SelectSingle<T>(string sql)
    {
        throw new NotImplementedException();
    }

    public Task<T> Upsert<T>(string sql)
    {
        throw new NotImplementedException();
    }
}