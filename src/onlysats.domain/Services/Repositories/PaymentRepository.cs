using onlysats.domain.Models;

namespace onlysats.domain.Services.Repositories;

/// <summary>
///
/// </summary>
public interface IPaymentRepository
{

}

#region Implementation

public class PaymentRepository : IPaymentRepository 
{
    private readonly SqlRepository _Repository;

    public PaymentRepository(OnlySatsConfiguration config)
    {
        _Repository = new SqlRepository(config.SqlConnectionString);
    }
}

#endregion 