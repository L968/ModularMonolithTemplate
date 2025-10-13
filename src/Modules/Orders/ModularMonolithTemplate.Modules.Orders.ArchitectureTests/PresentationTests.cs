using NetArchTest.Rules;
using ModularMonolithTemplate.Modules.Orders.ArchitectureTests.Abstractions;
using ModularMonolithTemplate.Common.Presentation.Endpoints;

namespace ModularMonolithTemplate.Modules.Orders.ArchitectureTests;

public class PresentationTests : BaseTest
{
    [Fact]
    public void Endpoint_Should_NotBePublic()
    {
        Types.InAssembly(PresentationAssembly)
            .That()
            .ImplementInterface(typeof(IEndpoint))
            .Should()
            .NotBePublic()
            .GetResult()
            .ShouldBeSuccessful();
    }

    [Fact]
    public void Endpoint_Should_BeSealed()
    {
        Types.InAssembly(PresentationAssembly)
            .That()
            .ImplementInterface(typeof(IEndpoint))
            .Should()
            .BeSealed()
            .GetResult()
            .ShouldBeSuccessful();
    }

    [Fact]
    public void Endpoint_ShouldHave_NameEndingWith_Endpoint()
    {
        Types.InAssembly(PresentationAssembly)
            .That()
            .ImplementInterface(typeof(IEndpoint))
            .Should()
            .HaveNameEndingWith("Endpoint")
            .GetResult()
            .ShouldBeSuccessful();
    }
}
