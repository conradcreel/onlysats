using Xunit;
using FluentAssertions;
using Moq;
using onlysats.domain.Services;
using onlysats.domain.Services.Repositories;
using onlysats.tests.Infrastructure;
using Dapr.Client;

namespace onlysats.tests;

public class OnboardingTests
{
    private Mock<IUserAccountRepository> _MockUserAccountRepository;
    private Mock<ICreatorRepository> _MockCreatorRepository;
    private Mock<IPatronRepository> _MockPatronRepository;
    private Mock<DaprClient> _MockDaprClient;

    private IOnboardingService _OnboardingService;
    
    public OnboardingTests()
    {
        _MockUserAccountRepository = new Mock<IUserAccountRepository>();
        _MockCreatorRepository = new Mock<ICreatorRepository>();
        _MockPatronRepository = new Mock<IPatronRepository>();
        _MockDaprClient = SetupInternalDependencies.SetupDaprClient();

        Setup();

        _OnboardingService = new OnboardingService (
            userAccountRepository: _MockUserAccountRepository.Object,
            creatorRepository: _MockCreatorRepository.Object,
            patronRepository: _MockPatronRepository.Object,
            daprClient: _MockDaprClient.Object
        );
    }

    private void Setup()
    {
        Assert.NotNull(_MockUserAccountRepository);
        Assert.NotNull(_MockCreatorRepository);
        Assert.NotNull(_MockPatronRepository);
        Assert.NotNull(_MockDaprClient);

        // TODO: Setup User Account Repository

        // TODO: Setup Creator Repository

        // TODO: Setup Patron Repository
    }

    [Fact]
    public void Test1()
    {
        Assert.Equal(1, 1);
    }
}