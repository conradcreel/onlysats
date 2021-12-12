namespace onlysats.domain.Services.Repositories;

/// <summary>
/// Encapsulates the SQL flavor implementation we choose so that the 
/// domain repositories can remain (mostly) ignorant. Once Dapr query 
/// stores go into at least Beta, this can be refactored to use Dapr.
/// </summary>
public class SqlRepository
{
    private readonly string _ConnectionString;

    public SqlRepository(string connectionString)
    {
        _ConnectionString = connectionString;
        // TODO: Rest of Driver-specific setup    
    }
}