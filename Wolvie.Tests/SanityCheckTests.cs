using System.Net;

using FluentAssertions;

namespace Wolvie.Tests;

public class SanityCheckTests(WolvieFactory wolvieFactory) : IClassFixture<WolvieFactory>, IDisposable
{
    private readonly HttpClient _client = wolvieFactory.CreateClient();

    [Fact]
    public async Task Swagger_WhenCalled_ReturnsOk()
    {
        var response = await _client.GetAsync("/swagger");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        _client.Dispose();
        wolvieFactory.Dispose();
    }
}