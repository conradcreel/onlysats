using Dapr.Client;
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

    private readonly DaprClient _DaprClient;

    public ContentManagementService(IAssetRepository assetRepository,
                                    IVaultRepository vaultRepository,
                                    IBlobRepository blobRepository,
                                    DaprClient daprClient)
    {
        _AssetRepository = assetRepository;
        _VaultRepository = vaultRepository;
        _BlobRepository = blobRepository;
        _DaprClient = daprClient;
    }
}

#endregion
