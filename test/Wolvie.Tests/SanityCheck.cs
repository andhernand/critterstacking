using System.Net;
using System.Net.Http.Json;

using Microsoft.AspNetCore.Mvc.Testing;

using Wolvie.Users;

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

    [Fact]
    public async Task CreateUser_WhenCalled_ReturnsCreatedAtRoute()
    {
        // Arrange
        var createMe = new CreateUser("test@test.org", "Test");

        // Act
        var response = await _client.PostAsJsonAsync("/users/create", createMe);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        response.Headers.Location?.PathAndQuery.Should().Contain("/users");
    }
}