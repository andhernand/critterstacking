using System.Net;

using FluentAssertions;

namespace Wolvie.Tests;

public class SanityCheck(WolvieFactory wolvieFactory) : IClassFixture<WolvieFactory>
{
    [Fact]
    public async Task Swagger_WhenCalled_ReturnsOk()
    {
        // Arrange
        var client = wolvieFactory.CreateClient();

        // Act
        var response = await client.GetAsync("/swagger");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}