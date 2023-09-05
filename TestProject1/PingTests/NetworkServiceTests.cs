using System.Net.NetworkInformation;
using FluentAssertions;
using FluentAssertions.Extensions;
using NetworkUtility.Ping;

namespace TestProject1.PingTests;

public class NetworkServiceTests
{
    private NetworkService _pingService;
    [SetUp]
    public void Setup()
    {
        _pingService = new NetworkService();
    }

    [Test]
    public void Test1()
    {
        Assert.Pass();
    }
    
     [Test]
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

    [Test]
    public void NetworkService_PingTimeout_ReturnInt()
    {
        // Arrange 
        int timeout_a = 1;
        int timeout_b = 1;
        int expected = 2;
        
        // Act
        var result = _pingService.PingTimeout(timeout_a, timeout_b);
        
        // Assert
        result.Should().Be(expected);
        result.Should().BeGreaterOrEqualTo(2);
        result.Should().NotBeInRange(-10000, 0);
    }
    
    
    [Test]
    public void NetworkService_PingTimeout_ReturnInt2()
    {
        // Arrange 
        int timeout_a = 2;
        int timeout_b = 2;
        int expected = 4;
        
        // Act
        var result = _pingService.PingTimeout(timeout_a, timeout_b);
        
        // Assert
        result.Should().Be(expected);
        result.Should().BeGreaterOrEqualTo(2);
        result.Should().NotBeInRange(-10000, 0);
    }

   [Test]
    public void NetworkService_LastPingDate_ReturnsDate()
    {
        // Arrange 
        
        // Act 
        var result = _pingService.LastPingDate();

        // Assert - Use Fluent Assertions rather than inbuilt Xunit assertions
        result.Should().BeAfter(1.January(2010));
        result.Should().BeBefore(1.January(2030));

    }

    [Test]
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

    [Test]
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