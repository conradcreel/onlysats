using System;
using System.Threading;
using BTCPayServer.Client.Models;
using Moq;
using onlysats.domain.Services;
using onlysats.domain.Services.Repositories;

namespace onlysats.tests.Infrastructure;

public static class SetupExternalDependencies
{
    public static Mock<ISqlRepository> SetupSqlRepository()
    {
        var mock = new Mock<ISqlRepository>();

        // TODO: Setup

        return mock;
    }

    public static Mock<IBlobRepository> SetupBlobRepository()
    {
        var mock = new Mock<IBlobRepository>();

        // TODO: Setup

        return mock;
    }

    public static Mock<IBitcoinPaymentProcessor> SetupBitcoinPaymentProcessor()
    {
        var mock = new Mock<IBitcoinPaymentProcessor>();

        mock.Setup(b => b.CreateAccount(It.IsAny<CreateStoreRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((CreateStoreRequest request, CancellationToken cancellationToken) =>
            {
                return new StoreData
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = request.Name
                };
            });


        mock.Setup(b => b.UpdateOnChainPaymentMethod(It.IsAny<string>(), It.IsAny<UpdateOnChainPaymentMethodRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((string accountId, UpdateOnChainPaymentMethodRequest request, CancellationToken cancellationToken) =>
            {
                return new OnChainPaymentMethodData
                {
                    
                };
            });

        //TODO: Setup rest of methods

        return mock;
    }
}