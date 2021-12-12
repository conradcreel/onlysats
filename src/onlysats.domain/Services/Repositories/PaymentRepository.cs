using onlysats.domain.Models;

namespace onlysats.domain.Services.Repositories;

/// <summary>
/// Encapsulates persistence of Payments and Payment methods (wallets)
/// </summary>
public interface IPaymentRepository
{

}

#region Implementation

public class PaymentRepository : IPaymentRepository 
{
    private readonly ISqlRepository _Repository;

    public PaymentRepository(ISqlRepository sqlRepository)
    {
        _Repository = sqlRepository;
    }
}

#endregion 