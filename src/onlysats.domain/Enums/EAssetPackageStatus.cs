namespace onlysats.domain.Enums;

/// <summary>
/// An Asset Package can be in Draft during creation and refinement, 
/// active where it's visible to all Patrons and Inactive where it's 
/// only visible to the Creator and further purchases are not allowed
/// </summary>
public enum EAssetPackageStatus
{
    DRAFT = 1,
    ACTIVE = 2,
    INACTIVE = 3
}