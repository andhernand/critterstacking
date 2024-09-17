using System.Net.Mime;

using Microsoft.AspNetCore.Http.HttpResults;

using Wolverine;

using Wolvie.Issues.Commands;
using Wolvie.Models;
using Wolvie.Repositories;

namespace Wolvie.Issues;

public static class IssuesEndpoints
{
    private const string GetById = "{id:regex(^[0-9a-hA-Hj-kJ-Km-nM-Np-tP-Tv-zV-Z]+$):length(26)}";

    public static void MapIssuesEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var issuesGroup = endpoints.MapGroup("issues").WithTags("Issues");

        issuesGroup.MapPost("create", CreateIssue)
            .WithName("CreateIssue")
            .WithDescription("Create a new issue")
            .Accepts<CreateIssue>(contentType: MediaTypeNames.Application.Json)
            .WithOpenApi();

        issuesGroup.MapPost("assign", AssignIssue)
            .WithName("AssignIssue")
            .WithDescription("Assign an Issue to a User")
            .Accepts<AssignIssue>(contentType: MediaTypeNames.Application.Json)
            .WithOpenApi();

        issuesGroup.MapGet(GetById, GetIssueById)
            .WithName("GetIssue")
            .WithDescription("Get an Issue")
            .WithOpenApi();
    }

    private static async Task<CreatedAtRoute> CreateIssue(CreateIssue body, IMessageBus bus)
    {
        var created = await bus.InvokeAsync<Issue>(body);
        return TypedResults.CreatedAtRoute("GetIssue", new { id = created.Id });
    }

    private static async Task<Ok> AssignIssue(AssignIssue body, IMessageBus bus)
    {
        await bus.InvokeAsync(body);
        return TypedResults.Ok();
    }

    private static Results<Ok<Issue>, NotFound> GetIssueById(string id, IssueRepository repository)
    {
        if (!Ulid.TryParse(id, out var ulid))
        {
            return TypedResults.NotFound();
        }

        var issue = repository.Get(ulid);
        return issue is null
            ? TypedResults.NotFound()
            : TypedResults.Ok(issue);
    }
}