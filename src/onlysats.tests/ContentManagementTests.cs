using Xunit;
using FluentAssertions;
using onlysats.domain.Services;
using Moq;
using onlysats.domain.Services.Repositories;

namespace onlysats.tests;

public class ContentManagementTests
{
    private Mock<IAssetRepository> _MockAssetRepository;
    private Mock<IVaultRepository> _MockVaultRepository;
    private Mock<IBlobRepository> _MockBlobRepository;

    private IContentManagementService _ContentManagementService;

    public ContentManagementTests()
    {
        _MockAssetRepository = new Mock<IAssetRepository>();
        _MockVaultRepository = new Mock<IVaultRepository>();
        _MockBlobRepository = new Mock<IBlobRepository>();

        Setup();

        _ContentManagementService = new ContentManagementService(
            assetRepository: _MockAssetRepository.Object,
            vaultRepository: _MockVaultRepository.Object,
            blobRepository: _MockBlobRepository.Object
        );
    }

    private void Setup()
    {
        Assert.NotNull(_MockAssetRepository);
        Assert.NotNull(_MockVaultRepository);
        Assert.NotNull(_MockBlobRepository);

        // TODO: Setup Asset Repository

        // TODO: Setup Vault Repository

        // TODO: Setup Blob Repository
    }

    [Fact]
    public void Test1()
    {
        Assert.Equal(1, 1);
    }
}