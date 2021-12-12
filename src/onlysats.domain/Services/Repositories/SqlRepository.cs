using onlysats.domain.Models;

namespace onlysats.domain.Services.Repositories;

/// <summary>
/// Encapsulates the SQL flavor implementation we choose so that the 
/// domain repositories can remain (mostly) ignorant. Once Dapr query 
/// stores go into at least Beta, this can be refactored to use Dapr.
/// </summary>
public interface ISqlRepository
{

}


public class SqlRepository : ISqlRepository
{
    private readonly string _ConnectionString;

    public SqlRepository(OnlySatsConfiguration config)
    {
        // TODO: Remove this, won't need to store reference to connection string
        _ConnectionString = config.SqlConnectionString;
        // TODO: Rest of Driver-specific setup    
    }
}