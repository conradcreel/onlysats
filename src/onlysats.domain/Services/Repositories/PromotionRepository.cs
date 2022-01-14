using onlysats.domain.Entity;

namespace onlysats.domain.Services.Repositories;


/// <summary>
///
/// </summary>
public interface IPromotionRepository
{
    Task<Promotion> GetPromotion(int promotionId);
    Task<Promotion> UpsertPromotion(Promotion promotion);
}

public class PromotionRepository : IPromotionRepository
{
    public Task<Promotion> GetPromotion(int promotionId)
    {
        throw new NotImplementedException();
    }

    public Task<Promotion> UpsertPromotion(Promotion promotion)
    {
        throw new NotImplementedException();
    }
}