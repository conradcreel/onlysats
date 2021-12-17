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
    private Mock<IBitcoinPaymentProcessor> _MockBitcoinPaymentProcessor;
    private Mock<MessagePublisherProxy> _MockMessagePublisher;
    private IAccountingService _AccountingService;

    public AccountingTests()
    {
        _MockPaymentRepository = new Mock<IPaymentRepository>();
        _MockBitcoinPaymentProcessor = SetupExternalDependencies.SetupBitcoinPaymentProcessor();
        _MockMessagePublisher = SetupInternalDependencies.SetupMessagePublisher();

        Setup();

        _AccountingService = new AccountingService(
            paymentRepository: _MockPaymentRepository.Object,
            bitcoinPaymentProcessor: _MockBitcoinPaymentProcessor.Object,
            messagePublisher: _MockMessagePublisher.Object
        );
    }

    private void Setup()
    {
        Assert.NotNull(_MockPaymentRepository);
        Assert.NotNull(_MockBitcoinPaymentProcessor);
        Assert.NotNull(_MockMessagePublisher);

        // TODO: Setup Payment Repository

        // TODO: Setup BtcPayServerProxy
    }

    [Fact]
    public async Task Setup_Wallet_Without_XPubKey_Returns_BadRequest()
    {
        Assert.Equal(1, 1);
    }
}