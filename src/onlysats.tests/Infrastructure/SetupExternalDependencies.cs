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

        // TODO: Setup

        return mock;
    }
}