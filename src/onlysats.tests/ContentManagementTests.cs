using Xunit;
using onlysats.domain.Services;
using Moq;
using onlysats.domain.Services.Repositories;
using onlysats.tests.Infrastructure;
using FluentAssertions;
using onlysats.domain.Models;
using System.Collections.Generic;
using onlysats.domain.Entity;
using System.Threading.Tasks;
using onlysats.domain.Services.Request.ContentManagement;
using onlysats.domain.Constants;

namespace onlysats.tests
{

    public class ContentManagementTests
    {
        private const int EXISTING_VAULT_ID = 100;
        private const int NONEXISTING_VAULT_ID = 98;

        private const int EXISTING_ASSET_ID = 200;
        private const int NONEXISTING_ASSET_ID = 2;

        private const int EXISTING_ASSET_PACKAGE_ID = 300;
        private const int NONEXISTING_ASSET_PACKAGE_ID = 3;

        private const int EXISTING_CREATOR_ID = 400;
        // pass this Creator on endpoints where we want to 
        // ensure that unauthorized creators are denied access
        private const int UNAUTHORIZED_CREATOR_ID = 499;
        private const int NONEXISTING_CREATOR_ID = 4;

        private Mock<IAssetRepository> _MockAssetRepository;
        private Mock<IVaultRepository> _MockVaultRepository;
        private Mock<IBlobRepository> _MockBlobRepository;
        private Mock<ICreatorRepository> _MockCreatorRepository;
        private Mock<IPatronRepository> _MockPatronRepository;
        private Mock<IMessagePublisher> _MockMessagePublisher;

        private IContentManagementService _ContentManagementService;

        public ContentManagementTests()
        {
            _MockAssetRepository = new Mock<IAssetRepository>();
            _MockVaultRepository = new Mock<IVaultRepository>();
            _MockBlobRepository = new Mock<IBlobRepository>();
            _MockCreatorRepository = new Mock<ICreatorRepository>();
            _MockPatronRepository = new Mock<IPatronRepository>();
            _MockMessagePublisher = SetupInternalDependencies.SetupMessagePublisher();

            Setup();

            _ContentManagementService = new ContentManagementService(
                assetRepository: _MockAssetRepository.Object,
                vaultRepository: _MockVaultRepository.Object,
                creatorRepository: _MockCreatorRepository.Object,
                patronRepository: _MockPatronRepository.Object,
                blobRepository: _MockBlobRepository.Object,
                messagePublisher: _MockMessagePublisher.Object
            );
        }

        private void Setup()
        {
            Assert.NotNull(_MockAssetRepository);
            Assert.NotNull(_MockVaultRepository);
            Assert.NotNull(_MockCreatorRepository);
            Assert.NotNull(_MockBlobRepository);
            Assert.NotNull(_MockMessagePublisher);

            _MockAssetRepository
                .Setup(a => a.GetAssetsInPackage(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync((int assetPackageId, int top, int skip) =>
                {
                    if (assetPackageId == EXISTING_ASSET_PACKAGE_ID)
                    {
                        return new List<Asset>
                        {
                        new Asset {
                            CreatorId = EXISTING_CREATOR_ID,
                            Id = EXISTING_ASSET_ID,
                            VaultId = EXISTING_VAULT_ID
                        }
                        };
                    }

                    return null;
                });

            _MockAssetRepository
                .Setup(a => a.GetAssetPackages(It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<List<int>>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync((int? creatorId, int? vaultId, List<int> assetPackageIds, int top, int skip) =>
                {
                    if ((creatorId == null || creatorId == EXISTING_CREATOR_ID) &&
                        (vaultId == null || vaultId == EXISTING_VAULT_ID) &&
                        (assetPackageIds == null || assetPackageIds.Contains(EXISTING_ASSET_PACKAGE_ID)))
                    {
                        return new List<AssetPackage>
                        {
                        new AssetPackage
                        {
                            CreatorId = EXISTING_CREATOR_ID,
                            Id = EXISTING_ASSET_PACKAGE_ID,
                            AssetIds = new List<int>{EXISTING_ASSET_ID},
                            VaultId = EXISTING_VAULT_ID
                        }
                        };
                    }

                    return null;
                });

            _MockAssetRepository
                .Setup(a => a.GetAsset(It.IsAny<int>()))
                .ReturnsAsync((int assetId) =>
                {
                    if (assetId == EXISTING_ASSET_ID)
                    {
                        return new Asset
                        {
                            CreatorId = EXISTING_CREATOR_ID,
                            Id = EXISTING_ASSET_ID,
                            VaultId = EXISTING_VAULT_ID
                        };
                    }

                    return null;
                });

            _MockAssetRepository
                .Setup(a => a.GetAssets(It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<List<int>>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync((int? creatorId, int? vaultId, List<int> assetIds, int top, int skip) =>
                {
                    if ((creatorId == null || creatorId == EXISTING_CREATOR_ID) &&
                        (vaultId == null || vaultId == EXISTING_VAULT_ID) &&
                        (assetIds == null || assetIds.Contains(EXISTING_ASSET_ID)))
                    {
                        return new List<Asset>
                        {
                        new Asset {
                            CreatorId = EXISTING_CREATOR_ID,
                            Id = EXISTING_ASSET_ID,
                            VaultId = EXISTING_VAULT_ID
                        }
                        };
                    }

                    return null;
                });

            _MockAssetRepository
                .Setup(a => a.UpsertAsset(It.IsAny<Asset>()))
                .ReturnsAsync((Asset asset) =>
                {
                    if (asset.Id <= 0)
                    {
                        asset.Id = EXISTING_ASSET_ID;
                    }

                    return asset;
                });

            _MockAssetRepository
                .Setup(a => a.UpsertAssetPackage(It.IsAny<AssetPackage>()))
                .ReturnsAsync((AssetPackage assetPackage) =>
                {
                    if (assetPackage.Id <= 0)
                    {
                        assetPackage.Id = EXISTING_ASSET_PACKAGE_ID;
                    }

                    return assetPackage;
                });

            _MockVaultRepository
                .Setup(v => v.GetVault(It.IsAny<int>()))
                .ReturnsAsync((int vaultId) =>
                {
                    if (vaultId == EXISTING_VAULT_ID)
                    {
                        return new Vault
                        {
                            Id = EXISTING_VAULT_ID,
                            CreatorId = EXISTING_CREATOR_ID
                        };
                    }

                    return null;
                });

            _MockVaultRepository
                .Setup(v => v.GetVaults(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync((int creatorId, int top, int skip) =>
                {
                    if (creatorId == EXISTING_CREATOR_ID)
                    {
                        return new List<Vault>
                        {
                        new Vault
                        {
                            Id = EXISTING_VAULT_ID,
                            CreatorId = EXISTING_CREATOR_ID
                        }
                        };
                    }
                    return null;
                });

            _MockVaultRepository
                .Setup(v => v.UpsertVault(It.IsAny<Vault>()))
                .ReturnsAsync((Vault vault) =>
                {
                    if (vault.Id <= 0)
                    {
                        vault.Id = EXISTING_VAULT_ID;
                    }

                    return vault;
                });

            // TODO: Setup Blob Repository

            _MockCreatorRepository
                .Setup(c => c.GetCreatorDetail(It.IsAny<int>()))
                .ReturnsAsync((int creatorId) =>
                {
                    if (creatorId == EXISTING_CREATOR_ID)
                    {
                        return new CreatorModel
                        {
                            CreatorId = creatorId,
                            Username = "satoshi"
                        };
                    }
                    else if (creatorId == UNAUTHORIZED_CREATOR_ID)
                    {
                        return new CreatorModel
                        {
                            CreatorId = UNAUTHORIZED_CREATOR_ID,
                            Username = "unauthd"
                        };
                    }

                    return null;
                });

            // TODO: Setup Patron Repository
        }

        [Fact]
        public async Task Cannot_Retrieve_Asset_Package_Without_Creator()
        {
            var request = new GetAssetPackagesRequest
            {
                AssetPackageIds = new List<int> { EXISTING_ASSET_PACKAGE_ID }
            };

            var response = await _ContentManagementService.GetAssetPackages(request);
            response.Should().NotBeNull();
            response.ResponseDetails.Should().NotBeNull();
            response?.ResponseDetails?.IsSuccess.Should().BeFalse();
            response?.ResponseDetails?.StatusCode.Should().Be(CResponseStatus.BAD_REQUEST);
        }

        [Fact]
        public async Task Cannot_Retrieve_Asset_Package_Without_Valid_Creator()
        {
            var request = new GetAssetPackagesRequest
            {
                CreatorId = NONEXISTING_CREATOR_ID
            };

            var response = await _ContentManagementService.GetAssetPackages(request);
            response.Should().NotBeNull();
            response.ResponseDetails.Should().NotBeNull();
            response?.ResponseDetails?.IsSuccess.Should().BeFalse();
            response?.ResponseDetails?.StatusCode.Should().Be(CResponseStatus.NOT_FOUND);
        }

        [Fact]
        public async Task Creator_Can_Get_Asset_Package_By_Id()
        {
            var request = new GetAssetPackagesRequest
            {
                CreatorId = EXISTING_CREATOR_ID,
                AssetPackageIds = new List<int> { EXISTING_ASSET_PACKAGE_ID }
            };

            var response = await _ContentManagementService.GetAssetPackages(request);
            response.Should().NotBeNull();
            response?.ResponseDetails?.IsSuccess.Should().BeTrue();
            response?.CreatorId.Should().Be(request.CreatorId);
            response?.AssetPackages.Count.Should().BeGreaterThan(0);
            response?.AssetPackages[0].AssetPackageId.Should().Be(EXISTING_ASSET_PACKAGE_ID);
        }

        [Fact]
        public async Task Creator_Cannot_Retrieve_Another_Creators_Asset_Package()
        {
            var request = new GetAssetPackagesRequest
            {
                CreatorId = UNAUTHORIZED_CREATOR_ID,
                AssetPackageIds = new List<int> { EXISTING_ASSET_PACKAGE_ID }
            };

            var response = await _ContentManagementService.GetAssetPackages(request);
            response.Should().NotBeNull();
            response.ResponseDetails.Should().NotBeNull();
            response?.ResponseDetails?.IsSuccess.Should().BeFalse();
            response?.ResponseDetails?.StatusCode.Should().Be(CResponseStatus.UNAUTHORIZED);
        }

        [Fact]
        public async Task Creator_Can_Retrieve_Asset_Packages_In_Vault()
        {
            var request = new GetAssetPackagesRequest
            {
                CreatorId = EXISTING_CREATOR_ID,
                VaultId = EXISTING_VAULT_ID
            };

            var response = await _ContentManagementService.GetAssetPackages(request);
            response.Should().NotBeNull();
            response?.ResponseDetails?.IsSuccess.Should().BeTrue();
            response?.CreatorId.Should().Be(request.CreatorId);
            response?.AssetPackages.Count.Should().BeGreaterThan(0);
            response?.AssetPackages[0].AssetPackageId.Should().Be(EXISTING_ASSET_PACKAGE_ID);
        }

        [Fact]
        public async Task Creator_Does_Not_Retrieve_Asset_Packages_From_Nonexisting_Vault()
        {
            var request = new GetAssetPackagesRequest
            {
                CreatorId = EXISTING_CREATOR_ID,
                VaultId = NONEXISTING_VAULT_ID
            };

            var response = await _ContentManagementService.GetAssetPackages(request);
            response.Should().NotBeNull();
            response.ResponseDetails.Should().NotBeNull();
            response?.ResponseDetails?.IsSuccess.Should().BeFalse();
            response?.ResponseDetails?.StatusCode.Should().Be(CResponseStatus.NOT_FOUND);
        }

        [Fact]
        public async Task Retrieving_Nonexisting_Asset_Package_Returns_Not_Found()
        {
            var request = new GetAssetPackagesRequest
            {
                CreatorId = EXISTING_CREATOR_ID,
                AssetPackageIds = new List<int> { NONEXISTING_ASSET_PACKAGE_ID }
            };

            var response = await _ContentManagementService.GetAssetPackages(request);
            response.Should().NotBeNull();
            response.ResponseDetails.Should().NotBeNull();
            response?.ResponseDetails?.IsSuccess.Should().BeFalse();
            response?.ResponseDetails?.StatusCode.Should().Be(CResponseStatus.NOT_FOUND);
        }

        [Fact]
        public async Task Cannot_Retrieve_Asset_Without_Creator()
        {
            var request = new GetAssetsRequest
            {
                AssetIds = new List<int> { EXISTING_ASSET_ID }
            };

            var response = await _ContentManagementService.GetAssets(request);
            response.Should().NotBeNull();
            response.ResponseDetails.Should().NotBeNull();
            response?.ResponseDetails?.IsSuccess.Should().BeFalse();
            response?.ResponseDetails?.StatusCode.Should().Be(CResponseStatus.BAD_REQUEST);
        }

        [Fact]
        public async Task Cannot_Retrieve_Asset_Without_Valid_Creator()
        {
            var request = new GetAssetsRequest
            {
                CreatorId = NONEXISTING_CREATOR_ID
            };

            var response = await _ContentManagementService.GetAssets(request);
            response.Should().NotBeNull();
            response.ResponseDetails.Should().NotBeNull();
            response?.ResponseDetails?.IsSuccess.Should().BeFalse();
            response?.ResponseDetails?.StatusCode.Should().Be(CResponseStatus.NOT_FOUND);
        }

        [Fact]
        public async Task Creator_Can_Get_Asset_By_Id()
        {
            var request = new GetAssetsRequest
            {
                CreatorId = EXISTING_CREATOR_ID,
                AssetIds = new List<int> { EXISTING_ASSET_ID }
            };

            var response = await _ContentManagementService.GetAssets(request);
            response.Should().NotBeNull();
            response?.ResponseDetails?.IsSuccess.Should().BeTrue();
            response?.CreatorId.Should().Be(request.CreatorId);
            response?.Assets.Count.Should().BeGreaterThan(0);
            response?.Assets[0].AssetId.Should().Be(EXISTING_ASSET_ID);
        }

        [Fact]
        public async Task Creator_Cannot_Retrieve_Another_Creators_Asset()
        {
            var request = new GetAssetsRequest
            {
                CreatorId = UNAUTHORIZED_CREATOR_ID,
                AssetIds = new List<int> { EXISTING_ASSET_ID }
            };

            var response = await _ContentManagementService.GetAssets(request);
            response.Should().NotBeNull();
            response.ResponseDetails.Should().NotBeNull();
            response?.ResponseDetails?.IsSuccess.Should().BeFalse();
            response?.ResponseDetails?.StatusCode.Should().Be(CResponseStatus.UNAUTHORIZED);
        }

        [Fact]
        public async Task Creator_Can_Retrieve_Assets_In_Vault()
        {
            var request = new GetAssetsRequest
            {
                CreatorId = EXISTING_CREATOR_ID,
                VaultId = EXISTING_VAULT_ID
            };

            var response = await _ContentManagementService.GetAssets(request);
            response.Should().NotBeNull();
            response?.ResponseDetails?.IsSuccess.Should().BeTrue();
            response?.CreatorId.Should().Be(request.CreatorId);
            response?.Assets.Count.Should().BeGreaterThan(0);
            response?.Assets[0].AssetId.Should().Be(EXISTING_ASSET_ID);
        }

        [Fact]
        public async Task Creator_Does_Not_Retrieve_Assets_From_Nonexisting_Vault()
        {
            var request = new GetAssetsRequest
            {
                CreatorId = EXISTING_CREATOR_ID,
                VaultId = NONEXISTING_VAULT_ID
            };

            var response = await _ContentManagementService.GetAssets(request);
            response.Should().NotBeNull();
            response.ResponseDetails.Should().NotBeNull();
            response?.ResponseDetails?.IsSuccess.Should().BeFalse();
            response?.ResponseDetails?.StatusCode.Should().Be(CResponseStatus.NOT_FOUND);
        }

        [Fact]
        public async Task Retrieving_Nonexisting_Asset_Returns_Not_Found()
        {
            var request = new GetAssetsRequest
            {
                CreatorId = EXISTING_CREATOR_ID,
                AssetIds = new List<int> { NONEXISTING_ASSET_ID }
            };

            var response = await _ContentManagementService.GetAssets(request);
            response.Should().NotBeNull();
            response.ResponseDetails.Should().NotBeNull();
            response?.ResponseDetails?.IsSuccess.Should().BeFalse();
            response?.ResponseDetails?.StatusCode.Should().Be(CResponseStatus.NOT_FOUND);
        }

        [Fact]
        public async Task Creator_Cannot_Create_New_Asset_Without_Required_Fields()
        {

        }

        [Fact]
        public async Task Creator_Must_Supply_At_Least_One_Field_When_Updating_Existing_Asset()
        {

        }

        [Fact]
        public async Task Creator_Can_Create_New_Asset()
        {

        }

        [Fact]
        public async Task Cannot_Create_Asset_Without_Creator()
        {

        }

        [Fact]
        public async Task Creator_Cannot_Update_Asset_That_Does_Not_Exist()
        {

        }

        [Fact]
        public async Task Creator_Cannot_Update_Another_Creators_Asset()
        {

        }

        [Fact]
        public async Task Creator_Cannot_Create_New_Asset_Package_Without_Required_Fields()
        {

        }

        [Fact]
        public async Task Creator_Must_Supply_At_Least_One_Field_When_Updating_Existing_Asset_Package()
        {

        }

        [Fact]
        public async Task Creator_Can_Create_New_Asset_Package()
        {

        }

        [Fact]
        public async Task Cannot_Create_Asset_Package_Without_Creator()
        {

        }

        [Fact]
        public async Task Creator_Cannot_Update_Asset_Package_That_Does_Not_Exist()
        {

        }

        [Fact]
        public async Task Creator_Cannot_Update_Another_Creators_Asset_Package()
        {

        }

    }
}