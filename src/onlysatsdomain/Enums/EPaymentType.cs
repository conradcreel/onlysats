namespace onlysats.domain.Enums
{
    /// <summary>
    /// The types of Payments a Patron can make to a Creator. These 
    /// payments can be subscriptions (pull payments), asset package
    /// purchases (direct payments), or tips (direct payments)
    /// </summary>
    public enum EPaymentType
    {
        SUBSCRIPTION = 1,
        ASSET_PACKAGE = 2,
        TIP = 3,
        PROMOTION = 4
    }
}