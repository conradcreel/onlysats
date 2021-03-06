using Xunit;
using FluentAssertions;
using Moq;
using onlysats.domain.Services;
using System.Threading.Tasks;
using onlysats.domain.Services.Repositories;
using onlysats.tests.Infrastructure;
using onlysats.domain.Entity;
using System.Collections.Generic;
using onlysats.domain.Services.Request.Accounting;
using onlysats.domain.Constants;

namespace onlysats.tests
{

    public class AccountingTests
    {
        private const int EXISTING_WALLET_ID = 123;
        private const int ACCOUNT_ID_WITH_NO_WALLETS = 100;
        private const int ACCOUNT_ID_WITH_EXISTING_WALLETS = 101;
        private const string BTC_PAY_PROCESSOR_ACCOUNT_ID = "btc123";

        private Mock<IPaymentRepository> _MockPaymentRepository;
        private Mock<IBitcoinPaymentProcessor> _MockBitcoinPaymentProcessor;
        private Mock<IMessagePublisher> _MockMessagePublisher;
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
                messagePublisher: _MockMessagePublisher.Object,
                chatRepository: null,
                chatService: null,
                chatHubContext: null
            );
        }

        private void Setup()
        {
            Assert.NotNull(_MockPaymentRepository);
            Assert.NotNull(_MockBitcoinPaymentProcessor);
            Assert.NotNull(_MockMessagePublisher);

            // TODO: Setup Payment Repository
            _MockPaymentRepository
                .Setup(p => p.UpsertWallet(It.IsAny<Wallet>()))
                .ReturnsAsync((Wallet wallet) =>
                {
                    if (wallet.Id == 0)
                    {
                        wallet.Id = EXISTING_WALLET_ID;
                    }

                    return wallet;
                });

            _MockPaymentRepository
                .Setup(p => p.GetWallets(It.IsAny<int>()))
                .ReturnsAsync((int userAccountId) =>
                {
                    if (userAccountId == ACCOUNT_ID_WITH_NO_WALLETS)
                    {
                        return new List<Wallet>();
                    }
                    else if (userAccountId == ACCOUNT_ID_WITH_EXISTING_WALLETS)
                    {
                        return new List<Wallet>
                        {
                        new Wallet {
                            Id = EXISTING_WALLET_ID,
                            UserAccountId = ACCOUNT_ID_WITH_EXISTING_WALLETS,
                            Nickname = "TODO",
                            BtcPayServerAccountId = BTC_PAY_PROCESSOR_ACCOUNT_ID
                        }
                        };
                    }

                    return default;
                });
        }
    }
}