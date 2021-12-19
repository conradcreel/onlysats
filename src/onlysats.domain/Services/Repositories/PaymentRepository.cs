using onlysats.domain.Entity;
using onlysats.domain.Models;

namespace onlysats.domain.Services.Repositories;

/// <summary>
/// Encapsulates persistence of Payments and Payment methods (wallets)
/// </summary>
public interface IPaymentRepository
{
    Task<IEnumerable<Wallet>> GetWallets(int userAccountId);
    Task<Wallet> UpsertWallet(Wallet wallet);
}

#region Implementation

public class PaymentRepository : IPaymentRepository 
{
    private readonly ISqlRepository _Repository;

    public PaymentRepository(ISqlRepository sqlRepository)
    {
        _Repository = sqlRepository;
    }

    public Task<IEnumerable<Wallet>> GetWallets(int userAccountId)
    {
        throw new NotImplementedException();
    }

    public Task<Wallet> UpsertWallet(Wallet wallet)
    {
        throw new NotImplementedException();
    }
}

#endregion 