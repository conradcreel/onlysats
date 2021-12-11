namespace onlysats.domain.Enums;

/// <summary>
/// An Asset can be active or inactive. Active Assets 
/// can be added to an Asset Package. Inactive Assets 
/// are hidden when creating Packages. Note that deactivating
/// an Asset already included in an active Asset Package has 
/// no effect. 
/// </summary>
public enum EAssetStatus
{
    ACTIVE = 1,
    INACTIVE = 2
}
