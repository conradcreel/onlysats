using onlysats.domain.Services.Repositories;

namespace onlysats.domain.Services;

/// <summary>
/// 
/// </summary>
public interface IContentManagementService
{

}

#region Implementation

public class ContentManagementService : IContentManagementService
{
    private readonly IAssetRepository _AssetRepository;
    private readonly IVaultRepository _VaultRepository;
    private readonly IBlobRepository _BlobRepository;

    public ContentManagementService(IAssetRepository assetRepository,
                                    IVaultRepository vaultRepository,
                                    IBlobRepository blobRepository)
    {
        _AssetRepository = assetRepository;
        _VaultRepository = vaultRepository;
        _BlobRepository = blobRepository;
    }
}

#endregion
