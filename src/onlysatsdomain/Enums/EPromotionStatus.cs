namespace onlysats.domain.Enums
{

    /// <summary>
    /// A Promotion can be in Draft during creation and refinement,
    /// once the Creator is satisfied with the Promotion they can 
    /// activate it which enables it to be sent out to Subscribers
    /// Inactive Promotions won't be shown in the default list of 
    /// Creator promotions
    /// </summary>
    public enum EPromotionStatus
    {
        DRAFT = 1,
        ACTIVE = 2,
        INACTIVE = 3
    }
}