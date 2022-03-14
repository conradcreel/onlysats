using onlysats.domain.Constants;
using onlysats.domain.Entity;
using onlysats.domain.Enums;
using onlysats.domain.Events;
using onlysats.domain.Models;
using onlysats.domain.Services.Repositories;
using onlysats.domain.Services.Request.ContentManagement;
using onlysats.domain.Services.Response;
using onlysats.domain.Services.Response.ContentManagement;
using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace onlysats.domain.Services
{

    /// <summary>
    /// Management of all Creator asset metadata and secure 
    /// URIs to retrieve and write to blobs (the raw asset content)
    /// </summary>
    public interface IContentManagementService
    {
        /// <summary>
        /// Retrieves all of a Creator's Vaults
        /// </summary>
        Task<GetVaultsResponse> GetVaults(GetVaultsRequest request);

        /// <summary>
        /// Sets the details of a Creator's Vault
        /// </summary>
        Task<SetVaultResponse> SetVault(SetVaultRequest request);

        /// <summary>
        /// Retrieves a Creator's Asset Package(s) depending on filters
        /// </summary>
        Task<GetAssetPackagesResponse> GetAssetPackages(GetAssetPackagesRequest request);

        /// <summary>
        /// Retrieves a Creator's Asset(s) depending on filters
        /// </summary>
        Task<GetAssetsResponse> GetAssets(GetAssetsRequest request);

        /// <summary>
        /// Sets Asset metadata and reference to Blob Storage identifiers.
        /// Note that the upload/handling of the binary data is done on the 
        /// client side and then we take the information from the Blob Storage
        /// SDK and persist it here (things like BlobId, URL, etc.)
        /// </summary>
        Task<SetAssetResponse> SetAsset(SetAssetRequest request);

        /// <summary>
        /// Sets the Asset Package details
        /// </summary>
        Task<SetAssetPackageResponse> SetAssetPackage(SetAssetPackageRequest request);

        /// <summary>
        /// Retrieves Assets a Patron has been sent or Purchased
        /// </summary>
        Task<GetPatronAssetsResponse> GetPatronAssets(GetPatronAssetsRequest request);

        /// <summary>
        /// Give a Patron access to a Creator's assets. It will create unique URLs
        /// for each Asset granted to the Patron.
        /// </summary>
        Task<SetPatronAssetsResponse> SetPatronAssets(SetPatronAssetsRequest request);
    }

    #region Implementation

    public class ContentManagementService : IContentManagementService
    {
        private readonly IAssetRepository _AssetRepository;
        private readonly IVaultRepository _VaultRepository;
        private readonly IBlobRepository _BlobRepository;
        private readonly IMessagePublisher _MessagePublisher;
        private readonly ICreatorRepository _CreatorRepository;
        private readonly IPatronRepository _PatronRepository;

        public ContentManagementService(IAssetRepository assetRepository,
                                        IVaultRepository vaultRepository,
                                        IBlobRepository blobRepository,
                                        ICreatorRepository creatorRepository,
                                        IPatronRepository patronRepository,
                                        IMessagePublisher messagePublisher)
        {
            _AssetRepository = assetRepository;
            _VaultRepository = vaultRepository;
            _BlobRepository = blobRepository;
            _CreatorRepository = creatorRepository;
            _PatronRepository = patronRepository;
            _MessagePublisher = messagePublisher;
        }

        public async Task<GetAssetPackagesResponse> GetAssetPackages(GetAssetPackagesRequest request)
        {
            if (request == null || !request.IsValid())
            {
                return new GetAssetPackagesResponse()
                            .BadRequest(CErrorMessage.GET_ASSET_PACKAGES_BAD_REQUEST);
            }

            var assetPackages = new List<AssetPackageModel>();

            // TODO: Could optimize this a bit since only need the Username, not all of the details.
            // Should be fine for now.
            var creator = await _CreatorRepository.GetCreatorDetail(request.CreatorId);

            if (creator == null)
            {
                return new GetAssetPackagesResponse().NotFound();
            }

            if (request.AssetPackageIds.Any())
            {
                if (request.AssetPackageIds.Count == 1)
                {
                    var assets = await _AssetRepository.GetAssetsInPackage(request.AssetPackageIds[0]);
                    if (assets == null)
                    {
                        return new GetAssetPackagesResponse().NotFound();
                    }

                    // This is the only path where an Unauthorized is returned to ensure that no 
                    // Assets are returned to the wrong caller. Everywhere else does a hard check
                    // on CreatorId in the query and therefore will filter out assetPackages which
                    // do not belong to this Creator
                    if (assets.Any(a => a.CreatorId != creator.CreatorId))
                    {
                        return new GetAssetPackagesResponse()
                                    .Unauthorized();
                    }

                    assetPackages = (await _AssetRepository.GetAssetPackages(request.CreatorId, assetPackageIds: request.AssetPackageIds))
                                    ?.Select(ap => new AssetPackageModel(ap, creator.Username, assets))
                                    .ToList();
                }
                else
                {
                    assetPackages = (await _AssetRepository.GetAssetPackages(request.CreatorId, assetPackageIds: request.AssetPackageIds,
                                                                                top: request.Top, skip: request.Skip))
                                    ?.Select(ap => new AssetPackageModel(ap, creator.Username))
                                    .ToList();
                }

            }
            else if (request.VaultId != null && request.VaultId > 0)
            {
                // Get all Asset Packages in this Vault
                assetPackages = (await _AssetRepository.GetAssetPackages(request.CreatorId, vaultId: request.VaultId,
                                                                        top: request.Top, skip: request.Skip))
                                ?.Select(ap => new AssetPackageModel(ap, creator.Username))
                                .ToList();
            }
            else
            {
                // Get all Asset Packages for this Creator across all Vaults
                assetPackages = (await _AssetRepository.GetAssetPackages(request.CreatorId,
                                                                         top: request.Top, skip: request.Skip))
                                ?.Select(ap => new AssetPackageModel(ap, creator.Username))
                                .ToList();
            }

            if (assetPackages == null || assetPackages.Count == 0)
            {
                return new GetAssetPackagesResponse()
                            .NotFound();
            }

            var total = await _AssetRepository.GetAssetPackageCount(request.CreatorId, request.VaultId, request.AssetPackageIds);

            return new GetAssetPackagesResponse
            {
                CreatorId = request.CreatorId,
                AssetPackages = assetPackages,
                Top = request.Top,
                Skip = request.Top,
                Total = total
            }.OK();
        }

        public async Task<GetAssetsResponse> GetAssets(GetAssetsRequest request)
        {
            if (request == null || !request.IsValid())
            {
                return new GetAssetsResponse()
                            .BadRequest(CErrorMessage.GET_ASSETS_BAD_REQUEST);
            }

            var assets = new List<AssetModel>();

            // TODO: Could optimize this a bit since only need the Username, not all of the details.
            // Should be fine for now.
            var creator = await _CreatorRepository.GetCreatorDetail(request.CreatorId);

            if (creator == null)
            {
                return new GetAssetsResponse().NotFound();
            }

            if (request.AssetIds.Any())
            {
                if (request.AssetIds.Count == 1)
                {
                    var asset = await _AssetRepository.GetAsset(request.AssetIds[0]);

                    if (asset == null)
                    {
                        return new GetAssetsResponse().NotFound();
                    }

                    if (asset.CreatorId != creator.CreatorId)
                    {
                        return new GetAssetsResponse()
                                    .Unauthorized();
                    }

                    assets.Add(new AssetModel(asset, creator.Username));
                }
                else
                {
                    assets = (await _AssetRepository.GetAssets(request.CreatorId, assetIds: request.AssetIds,
                                                                top: request.Top, skip: request.Skip))
                                    ?.Select(a => new AssetModel(a, creator.Username))
                                    .ToList();
                }

            }
            else if (request.VaultId != null && request.VaultId > 0)
            {
                // Get all Assets in this Vault
                assets = (await _AssetRepository.GetAssets(request.CreatorId, vaultId: request.VaultId,
                                                            top: request.Top, skip: request.Skip))
                                ?.Select(a => new AssetModel(a, creator.Username))
                                .ToList();
            }
            else
            {
                // Get all Assets for this Creator across all Vaults
                assets = (await _AssetRepository.GetAssets(request.CreatorId,
                                                            top: request.Top, skip: request.Skip))
                                ?.Select(a => new AssetModel(a, creator.Username))
                                .ToList();
            }

            if (assets == null || assets.Count == 0)
            {
                return new GetAssetsResponse()
                            .NotFound();
            }

            var total = await _AssetRepository.GetAssetCount(request.CreatorId, request.VaultId, request.AssetIds);

            return new GetAssetsResponse
            {
                CreatorId = request.CreatorId,
                Assets = assets,
                Top = request.Top,
                Skip = request.Top,
                Total = total
            }.OK();
        }

        public async Task<GetPatronAssetsResponse> GetPatronAssets(GetPatronAssetsRequest request)
        {
            if (request == null || !request.IsValid())
            {
                return new GetPatronAssetsResponse()
                            .BadRequest(CErrorMessage.GET_PATRON_ASSETS_BAD_REQUEST);
            }

            var patronAssets = await _PatronRepository.GetPatronAssets(request.PatronId, request.CreatorId, request.AssetIds);

            if (!patronAssets.Any())
            {
                return new GetPatronAssetsResponse().NotFound();
            }

            var assetIds = patronAssets.Select(p => p.AssetId).ToList();

            if (request.AssetIds?.Count > 0 && request.AssetIds.Count != assetIds.Count)
            {
                return new GetPatronAssetsResponse().Unauthorized();
            }

            var assets = await _AssetRepository.GetAssets(assetIds: assetIds, top: request.Top, skip: request.Skip);

            if (assets == null)
            {
                return new GetPatronAssetsResponse().NotFound();
            }

            var patronAssetModels = new List<PatronAssetModel>();

            foreach (var asset in assets)
            {
                var patronAsset = patronAssets.FirstOrDefault(p => p.AssetId == asset.Id);
                if (patronAsset == null)
                {
                    continue;
                }

                patronAssetModels.Add(new PatronAssetModel(patronAsset, asset));
            }

            return new GetPatronAssetsResponse
            {
                Assets = patronAssetModels
            }.OK();
        }

        public async Task<GetVaultsResponse> GetVaults(GetVaultsRequest request)
        {
            if (request == null || !request.IsValid())
            {
                return new GetVaultsResponse()
                            .BadRequest(CErrorMessage.GET_VAULTS_BAD_REQUEST);
            }

            var vaults = new List<VaultModel>();

            // TODO: Could optimize this a bit since only need the Username, not all of the details.
            // Should be fine for now.
            var creator = await _CreatorRepository.GetCreatorDetail(request.CreatorId);

            if (creator == null)
            {
                return new GetVaultsResponse().NotFound();
            }

            if (request.VaultId != null && request.VaultId > 0)
            {
                // Get a single vault
                var vault = await _VaultRepository.GetVault(request.VaultId.Value);

                if (vault == null)
                {
                    return new GetVaultsResponse().NotFound();
                }

                if (vault.CreatorId != creator.CreatorId)
                {
                    return new GetVaultsResponse()
                                .Unauthorized();
                }

                // Get all assets. Note: Removing this for now, might include it if we find a use case for it
                // currently, they should just be calling GetAssets(vaultId, top, skip) so they can page through
                // assets once a vault is selected. Maybe in this endpoint we'll only need to return the number 
                // of assets or some other description.
                // var assets = await _AssetRepository.GetAssets(request.CreatorId, request.VaultId.Value, top: int.MaxValue, skip: 0);

                vaults.Add(new VaultModel(vault, creator.Username));
            }
            else
            {
                // Get all vaults for this Creator
                vaults = (await _VaultRepository.GetVaults(request.CreatorId, top: request.Top, skip: request.Skip))
                            ?.Select(v => new VaultModel(v, creator.Username))
                            .ToList();
            }

            if (vaults == null || vaults.Count == 0)
            {
                return new GetVaultsResponse()
                            .NotFound();
            }

            var total = await _VaultRepository.GetVaultCount(request.CreatorId);

            return new GetVaultsResponse
            {
                CreatorId = request.CreatorId,
                Vaults = vaults,
                Top = request.Top,
                Skip = request.Top,
                Total = total
            }.OK();
        }

        public async Task<SetAssetResponse> SetAsset(SetAssetRequest request)
        {
            if (request == null || !request.IsValid())
            {
                return new SetAssetResponse()
                            .BadRequest(CErrorMessage.SET_ASSET_BAD_REQUEST);
            }

            Asset asset;
            var creator = await _CreatorRepository.GetCreatorDetail(request.CreatorId);

            if (creator == null)
            {
                return new SetAssetResponse()
                            .NotFound();
            }

            if (request.AssetId != null)
            {
                // Update existing Asset
                asset = await _AssetRepository.GetAsset(request.AssetId.Value);

                if (asset == null)
                {
                    return new SetAssetResponse()
                                .NotFound();
                }

                if (asset.CreatorId != request.CreatorId)
                {
                    return new SetAssetResponse()
                                .Unauthorized();
                }

                // At this point we have an Asset that the caller is authorized to update

                if (request.VaultId != null)
                {
                    asset.VaultId = request.VaultId.Value;
                }

                if (request.FolderName != null)
                {
                    asset.FolderName = request.FolderName.Value;
                }

                if (request.Type != null)
                {
                    asset.Type = request.Type.Value;
                }

                if (request.DisplayName != null)
                {
                    asset.DisplayName = request.DisplayName.Value;
                }

                if (request.LocalHash != null)
                {
                    asset.LocalHash = request.LocalHash.Value;
                }

                if (request.RemoteLocation != null)
                {
                    asset.RemoteLocation = request.RemoteLocation.Value;
                }

                if (request.BlobId != null)
                {
                    asset.BlobId = request.BlobId.Value;
                }

                if (request.Status != null)
                {
                    if (asset.Status == EAssetStatus.ACTIVE && request.Status.Value == EAssetStatus.INACTIVE)
                    {
                        var domainEvent = new AssetDeactivatedEvent
                        {
                            CreatorId = asset.CreatorId,
                            AssetId = asset.Id,
                            AssetName = asset.DisplayName,
                            VaultId = asset.VaultId
                        };

                        await _MessagePublisher.PublishEvent(domainEvent.Topic, domainEvent);
                    }
                    else if (asset.Status == EAssetStatus.INACTIVE && request.Status.Value == EAssetStatus.ACTIVE)
                    {
                        var domainEvent = new AssetPublishedEvent
                        {
                            CreatorId = asset.CreatorId,
                            AssetId = asset.Id,
                            AssetName = asset.DisplayName,
                            VaultId = asset.VaultId
                        };

                        await _MessagePublisher.PublishEvent(domainEvent.Topic, domainEvent);
                    }

                    asset.Status = request.Status.Value;
                }

                asset = await _AssetRepository.UpsertAsset(asset);

                if (asset == null)
                {
                    return new SetAssetResponse()
                                .ServerError(CErrorMessage.SET_ASSET_COULD_NOT_UPDATE);
                }
            }
            else
            {
                // New Asset
                asset = await _AssetRepository.UpsertAsset(new Asset
                {
                    CreatorId = request.CreatorId,
                    VaultId = request.VaultId.Value,
                    FolderName = request.FolderName.Value,
                    Type = request.Type.Value,
                    DisplayName = request.DisplayName.Value,
                    Description = request.Description.Value,
                    LocalHash = request.LocalHash.Value,
                    RemoteLocation = request.RemoteLocation.Value,
                    BlobId = request.BlobId.Value,
                    Status = request.Status.Value
                });

                if (asset == null)
                {
                    return new SetAssetResponse()
                                .ServerError(CErrorMessage.SET_ASSET_COULD_NOT_CREATE);
                }

                if (asset.Status == EAssetStatus.ACTIVE)
                {
                    var domainEvent = new AssetPublishedEvent
                    {
                        CreatorId = asset.CreatorId,
                        AssetId = asset.Id,
                        AssetName = asset.DisplayName,
                        VaultId = asset.VaultId
                    };

                    await _MessagePublisher.PublishEvent(domainEvent.Topic, domainEvent);
                }
            }

            return new SetAssetResponse
            {
                Asset = new AssetModel(asset, creator.Username)
            }.OK();
        }

        public async Task<SetAssetPackageResponse> SetAssetPackage(SetAssetPackageRequest request)
        {
            if (request == null || !request.IsValid())
            {
                return new SetAssetPackageResponse()
                            .BadRequest(CErrorMessage.SET_ASSET_PACKAGE_BAD_REQUEST);
            }

            AssetPackage? assetPackage;
            var creator = await _CreatorRepository.GetCreatorDetail(request.CreatorId);

            if (creator == null)
            {
                return new SetAssetPackageResponse()
                            .NotFound();
            }

            if (request.AssetPackageId != null)
            {
                // Update existing AssetPackage
                assetPackage = await _AssetRepository.GetAssetPackage(request.AssetPackageId.Value);

                if (assetPackage == null)
                {
                    return new SetAssetPackageResponse()
                                .NotFound();
                }

                if (assetPackage.CreatorId != request.CreatorId)
                {
                    return new SetAssetPackageResponse()
                                .Unauthorized();
                }

                // At this point we have an Asset Package that the caller is authorized to update

                if (request.VaultId != null)
                {
                    assetPackage.VaultId = request.VaultId.Value;
                }

                if (request.DisplayName != null)
                {
                    assetPackage.DisplayName = request.DisplayName.Value;
                }

                if (request.Description != null)
                {
                    assetPackage.Description = request.Description.Value;
                }

                if (request.AssetIdsToAdd != null)
                {
                    var assetCheck = await _AssetRepository.GetAssets(request.CreatorId, assetIds: request.AssetIdsToAdd.Value, skip: 0, top: int.MaxValue);

                    if (assetCheck?.Count() != request.AssetIdsToAdd.Value.Count)
                    {
                        // Attempted to add at least one Asset to this package that the 
                        // Creator doesn't own.
                        return new SetAssetPackageResponse()
                                    .Unauthorized();
                    }

                    foreach (var assetId in request.AssetIdsToAdd.Value)
                    {
                        if (!assetPackage.AssetIds.Contains(assetId))
                        {
                            assetPackage.AssetIds.Add(assetId);
                        }
                    }
                }

                if (request.AssetIdsToRemove != null)
                {
                    foreach (var assetId in request.AssetIdsToRemove.Value)
                    {
                        assetPackage.AssetIds.Remove(assetId);
                    }
                }

                if (request.CoverPhotoUri != null)
                {
                    assetPackage.CoverPhotoUri = request.CoverPhotoUri.Value;
                }

                if (request.BuyNowPrice != null)
                {
                    assetPackage.BuyNowPrice = request.BuyNowPrice.Value;
                }

                if (request.IsLocked != null)
                {
                    assetPackage.IsLocked = request.IsLocked.Value;
                }

                if (request.Status != null)
                {
                    if (assetPackage.Status == EAssetPackageStatus.ACTIVE && request.Status.Value == EAssetPackageStatus.INACTIVE)
                    {
                        var domainEvent = new AssetPackageDeactivatedEvent
                        {
                            CreatorId = assetPackage.CreatorId,
                            AssetPackageId = assetPackage.Id,
                            AssetPackageName = assetPackage.DisplayName,
                            VaultId = assetPackage.VaultId
                        };

                        await _MessagePublisher.PublishEvent(domainEvent.Topic, domainEvent);
                    }
                    else if (assetPackage.Status == EAssetPackageStatus.INACTIVE && request.Status.Value == EAssetPackageStatus.ACTIVE)
                    {
                        var domainEvent = new AssetPackagePublishedEvent
                        {
                            CreatorId = assetPackage.CreatorId,
                            AssetPackageId = assetPackage.Id,
                            AssetPackageName = assetPackage.DisplayName,
                            VaultId = assetPackage.VaultId
                        };

                        await _MessagePublisher.PublishEvent(domainEvent.Topic, domainEvent);
                    }

                    assetPackage.Status = request.Status.Value;
                }

                assetPackage = await _AssetRepository.UpsertAssetPackage(assetPackage);

                if (assetPackage == null)
                {
                    return new SetAssetPackageResponse()
                                .ServerError(CErrorMessage.SET_ASSET_PACKAGE_COULD_NOT_UPDATE);
                }
            }
            else
            {
                // New Asset Package
                assetPackage = await _AssetRepository.UpsertAssetPackage(new AssetPackage
                {
                    CreatorId = request.CreatorId,
                    VaultId = request.VaultId.Value,
                    DisplayName = request.DisplayName.Value,
                    Description = request.Description.Value,
                    AssetIds = request.AssetIdsToAdd.Value,
                    CoverPhotoUri = request.CoverPhotoUri.Value,
                    BuyNowPrice = request.BuyNowPrice.Value,
                    IsLocked = request.IsLocked.Value,
                    Status = request.Status.Value
                });

                if (assetPackage == null)
                {
                    return new SetAssetPackageResponse()
                                .ServerError(CErrorMessage.SET_ASSET_PACKAGE_COULD_NOT_CREATE);
                }

                if (assetPackage.Status == EAssetPackageStatus.ACTIVE)
                {
                    var domainEvent = new AssetPackagePublishedEvent
                    {
                        CreatorId = assetPackage.CreatorId,
                        AssetPackageId = assetPackage.Id,
                        AssetPackageName = assetPackage.DisplayName,
                        VaultId = assetPackage.VaultId
                    };

                    await _MessagePublisher.PublishEvent(domainEvent.Topic, domainEvent);
                }
            }

            return new SetAssetPackageResponse
            {
                AssetPackage = new AssetPackageModel(assetPackage, creator.Username)
            }.OK();
        }

        public async Task<SetPatronAssetsResponse> SetPatronAssets(SetPatronAssetsRequest request)
        {
            if (request == null || !request.IsValid())
            {
                return new SetPatronAssetsResponse()
                            .BadRequest(CErrorMessage.SET_PATRON_ASSETS_BAD_REQUEST);
            }

            // TODO: 
            // 0. Filter out assets already granted to Patron (if more than one AssetId is provided)
            // 1. Retrieve Assets 
            // 2. Generate SAS URIs for each Asset
            // 3. Add each Asset as a Patron Asset via the BulkAdd repository method

            var existing = await _PatronRepository.GetPatronAssets(request.PatronId, request.CreatorId);
            var existingAssetIds = existing.Select(a => a.AssetId);

            var assetIds = request.AssetIds;
            if (assetIds.Count > 1 && existingAssetIds.Any())
            {
                assetIds = assetIds.Where(a => !existingAssetIds.Contains(a)).ToList();
            }

            var assets = await _AssetRepository.GetAssets(assetIds: assetIds, top: assetIds.Count);
            if (assets == null)
            {
                return new SetPatronAssetsResponse()
                            .BadRequest(CErrorMessage.SET_PATRON_ASSETS_NO_ASSETS_FOUND);
            }

            var sasUriMap = new Dictionary<int, string>();
            var assetMap = new Dictionary<int, Asset>();

            foreach (var asset in assets)
            {
                var sasUri = await _BlobRepository.GenerateSharedAccessSignature(asset.BlobId);
                sasUriMap.Add(asset.Id, sasUri);
                assetMap.Add(asset.Id, asset);
            }

            var now = DateTime.UtcNow;

            var patronAssets = await _PatronRepository.AddPatronAssetsBulk(
                assets.Select(a => new PatronAsset
                {
                    UniqueAssetUri = sasUriMap[a.Id],
                    PatronId = request.PatronId,
                    CreatorId = request.CreatorId,
                    PaymentId = request.PaymentId,
                    AssetPackageName = request.AssetPackageName,
                    AssetId = a.Id,
                    DateAcquired = now
                }).ToList()
            );


            return new SetPatronAssetsResponse
            {
                Assets = patronAssets.Select(p => new PatronAssetModel(p, assetMap[p.AssetId])).ToList()
            }.OK();
        }

        public async Task<SetVaultResponse> SetVault(SetVaultRequest request)
        {
            if (request == null || !request.IsValid())
            {
                return new SetVaultResponse()
                            .BadRequest(CErrorMessage.SET_VAULT_BAD_REQUEST);
            }

            Vault? vault;
            var creator = await _CreatorRepository.GetCreatorDetail(request.CreatorId);

            if (creator == null)
            {
                return new SetVaultResponse()
                            .NotFound();
            }

            if (request.VaultId != null)
            {
                // Update existing Vault
                vault = await _VaultRepository.GetVault(request.VaultId.Value);

                if (vault == null)
                {
                    return new SetVaultResponse()
                                .NotFound();
                }

                if (vault.CreatorId != request.CreatorId)
                {
                    return new SetVaultResponse()
                                .Unauthorized();
                }

                // At this point we have a Vault that the caller is authorized to update

                if (request.Name != null)
                {
                    vault.Name = request.Name.Value;
                }

                if (request.FoldersToAdd != null)
                {
                    foreach (var folder in request.FoldersToAdd.Value)
                    {
                        if (!vault.Folders.Contains(folder))
                        {
                            vault.Folders.Add(folder);
                        }
                    }
                }

                if (request.FoldersToRemove != null)
                {
                    // TODO: Figure out best way to handle this
                    // when there's Assets already included in this
                    // folder. For now we're just doing a dumb remove
                    foreach (var folder in request.FoldersToRemove.Value)
                    {
                        vault.Folders.Remove(folder);
                    }
                }

                if (request.Description != null)
                {
                    vault.Description = request.Description.Value;
                }

                vault = await _VaultRepository.UpsertVault(vault);

                if (vault == null)
                {
                    return new SetVaultResponse()
                                .ServerError(CErrorMessage.SET_VAULT_COULD_NOT_UPDATE);
                }
            }
            else
            {
                // New Vault
                vault = await _VaultRepository.UpsertVault(new Vault
                {
                    CreatorId = request.CreatorId,
                    Name = request.Name.Value,
                    Description = request.Description.Value,
                    Folders = request.FoldersToAdd.Value
                });

                if (vault == null)
                {
                    return new SetVaultResponse()
                                .ServerError(CErrorMessage.SET_VAULT_COULD_NOT_CREATE);
                }

                var domainEvent = new NewVaultCreatedEvent
                {
                    CreatorId = vault.CreatorId,
                    VaultId = vault.Id,
                    VaultName = vault.Name
                };

                await _MessagePublisher.PublishEvent(domainEvent.Topic, domainEvent);
            }

            return new SetVaultResponse
            {
                Vault = new VaultModel(vault, creator.Username)
            }.OK();
        }
    }

    #endregion
}