using System.Net.NetworkInformation;
using FluentAssertions;
using FluentAssertions.Extensions;
using Xunit.Abstractions;

namespace NetworkUtility.Tests.PingTests;

using NetworkUtility.Ping;
using Xunit;
public class NetworkServiceTests
{

    private readonly NetworkService _pingService;
    private readonly ITestOutputHelper _output;
    
    public NetworkServiceTests(ITestOutputHelper output)
    {
        // xUnit just allows setup to occur within the constructor of the test class. nUnit has a separate [SETUP]/[TEARDOWN] directive.
        // xUnit creates one instance of the test class per test whereas nUnit will use the same method.
        // SUT
        _output = output;
        _output.WriteLine("Constructor");
        _pingService = new NetworkService();
    }
    
    [Fact]
    public void NetworkService_SendPing_ReturnsString()
    {
        // Arrange 
        
        // Act 
        string result = _pingService.SendPing();

        // Assert - Use Fluent Assertions rather than inbuilt Xunit assertions
        result.Should().NotBeNullOrWhiteSpace();
        result.Should().Be("Success: Ping Sent!");
        result.Should().Contain("Success", Exactly.Once());

    }

    [Theory]
    [InlineData(1, 1, 2)]
    [InlineData(2, 2, 4)]
    public void NetworkService_PingTimeout_ReturnInt(int a, int b, int expected)
    {
        // Arrange 
        
        // Act
        var result = _pingService.PingTimeout(a, b);
        
        // Assert
        result.Should().Be(expected);
        result.Should().BeGreaterOrEqualTo(2);
        result.Should().NotBeInRange(-10000, 0);
    }
    
    [Fact]
    public void NetworkService_LastPingDate_ReturnsDate()
    {
        // Arrange 
        
        // Act 
        var result = _pingService.LastPingDate();

        // Assert - Use Fluent Assertions rather than inbuilt Xunit assertions
        result.Should().BeAfter(1.January(2010));
        result.Should().BeBefore(1.January(2030));

    }

    [Fact]
    public void NetworkService_GetPingOptions_ReturnsObject()
    {
        // Arrange
        var expected = new PingOptions()
        {
            DontFragment = true,
            Ttl = 1
        };
        // Act
        var result = _pingService.GetPingOptions();
        
        // Assert WARNING: "Be" careful (for reference variables)
        result.Should().BeOfType<PingOptions>();
        result.Should().BeEquivalentTo(expected);
        result.Ttl.Should().Be(1);

    }

    [Fact]
    public void NetworkService_MostRecentPings_ReturnsObjects()
    {
        // Arrange
        var expected = new PingOptions()
        {
            DontFragment = true,
            Ttl = 1
        };
        // Act
        var result = _pingService.MostRecentPings();
        
        // Assert WARNING: "Be" careful (for reference variables)
        result.Should().ContainEquivalentOf(expected);
        result.Should().Contain( x => x.DontFragment == true );

    }
    
}