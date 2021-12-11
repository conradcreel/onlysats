using Xunit;
using FluentAssertions;
using Moq;
using onlysats.domain.Services;

namespace onlysats.tests;

public class OnboardingTests
{
    private Mock<IOnboardingService>? _OnboardingServiceMock;

    public OnboardingTests()
    {
        Setup();
    }

    private void Setup()
    {
        _OnboardingServiceMock = new Mock<IOnboardingService>();
        
    }

    [Fact]
    public void Test1()
    {
        Assert.Equal(1, 1);
    }
}