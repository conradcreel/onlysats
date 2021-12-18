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
    private readonly IMessagePublisher _MessagePublisher;

    public ContentManagementService(IAssetRepository assetRepository,
                                    IVaultRepository vaultRepository,
                                    IBlobRepository blobRepository,
                                    IMessagePublisher messagePublisher)
    {
        _AssetRepository = assetRepository;
        _VaultRepository = vaultRepository;
        _BlobRepository = blobRepository;
        _MessagePublisher = messagePublisher;
    }
}

#endregion
