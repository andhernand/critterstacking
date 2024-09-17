using System.Net;
using System.Net.Http.Json;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc.Testing;

using Wolvie.Issues.Commands;
using Wolvie.Users.Commands;

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
    public async Task CreateUser_WhenCalled_ReturnsCreated()
    {
        // Arrange
        var creatUserCommand = new CreateUser("test@test.org", "Test");

        // Act
        var response = await _client.PostAsJsonAsync("/users/create", creatUserCommand);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        response.Headers.Location?.PathAndQuery.Should().Contain("/users");
    }

    [Fact]
    public async Task CreateIssue_WhenCalled_ReturnsCreated()
    {
        // Arrange
        var createIssueCommand = new CreateIssue
        {
            OriginatorId = Ulid.NewUlid(),
            Title = "Cannot login to system",
            Description = "I receive a BSOD everytime I try to log in."
        };

        // Act
        var response = await _client.PostAsJsonAsync("/issues/create", createIssueCommand);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        response.Headers.Location?.PathAndQuery.Should().Contain("/issues");
    }
}