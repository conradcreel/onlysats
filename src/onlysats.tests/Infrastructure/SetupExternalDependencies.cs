using Moq;
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
}