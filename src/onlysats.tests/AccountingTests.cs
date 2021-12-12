using Xunit;
using FluentAssertions;
using Moq;
using onlysats.domain.Services;
using System.Threading.Tasks;
using onlysats.domain.Services.Repositories;

namespace onlysats.tests;

public class AccountingTests
{
    private Mock<IPaymentRepository> _MockPaymentRepository;
    private Mock<BtcPayServerProxy> _MockBtcPayServerProxy;

    private IAccountingService _AccountingService;

    public AccountingTests()
    {
        _MockPaymentRepository = new Mock<IPaymentRepository>();
        _MockBtcPayServerProxy = new Mock<BtcPayServerProxy>();

        Setup();

        _AccountingService = new AccountingService(
            paymentRepository: _MockPaymentRepository.Object,
            btcPayProxy: _MockBtcPayServerProxy.Object
        );
    }

    private void Setup()
    {
        Assert.NotNull(_MockPaymentRepository);
        Assert.NotNull(_MockBtcPayServerProxy);
        
        // TODO: Setup Payment Repository

        // TODO: Setup BtcPayServerProxy
    }

    [Fact]
    public async Task Setup_Wallet_Without_XPubKey_Returns_BadRequest()
    {
        Assert.Equal(1, 1);
    }
}