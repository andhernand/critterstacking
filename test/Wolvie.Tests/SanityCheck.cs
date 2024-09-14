using System.Net;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc.Testing;

namespace Wolvie.Tests;

public class SanityCheck(WolvieFactory wolvieFactory) : IClassFixture<WolvieFactory>
{
    private readonly HttpClient _client = wolvieFactory.CreateClient(
        new WebApplicationFactoryClientOptions { BaseAddress = new Uri("https://localhost") });

    [Fact]
    public async Task Swagger_WhenCalled_ReturnsOk()
    {
        var response = await _client.GetAsync("/swagger/index.html");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}