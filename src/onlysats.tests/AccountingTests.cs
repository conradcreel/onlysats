using Xunit;
using FluentAssertions;
using Moq;
using onlysats.domain.Services;
using System.Threading.Tasks;
using onlysats.domain.Services.Repositories;
using Dapr.Client;
using onlysats.tests.Infrastructure;

namespace onlysats.tests;

public class AccountingTests
{
    private Mock<IPaymentRepository> _MockPaymentRepository;
    private Mock<BtcPayServerProxy> _MockBtcPayServerProxy;
    private Mock<DaprClient> _MockDaprClient;
    private IAccountingService _AccountingService;

    public AccountingTests()
    {
        _MockPaymentRepository = new Mock<IPaymentRepository>();
        _MockBtcPayServerProxy = new Mock<BtcPayServerProxy>();
        _MockDaprClient = SetupInternalDependencies.SetupDaprClient();

        Setup();

        _AccountingService = new AccountingService(
            paymentRepository: _MockPaymentRepository.Object,
            btcPayProxy: _MockBtcPayServerProxy.Object,
            daprClient: _MockDaprClient.Object
        );
    }

    private void Setup()
    {
        Assert.NotNull(_MockPaymentRepository);
        Assert.NotNull(_MockBtcPayServerProxy);
        Assert.NotNull(_MockDaprClient);

        // TODO: Setup Payment Repository

        // TODO: Setup BtcPayServerProxy
    }

    [Fact]
    public async Task Setup_Wallet_Without_XPubKey_Returns_BadRequest()
    {
        Assert.Equal(1, 1);
    }
}