using System.Net.Mime;

using Microsoft.AspNetCore.Http.HttpResults;

using Wolverine;

using Wolvie.Models;
using Wolvie.Repositories;
using Wolvie.Users.Commands;

namespace Wolvie.Users;

public static class UsersEndpoints
{
    private const string GetById = "{id:regex(^[0-9a-hA-Hj-kJ-Km-nM-Np-tP-Tv-zV-Z]+$):length(26)}";

    public static void MapUsersEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var usersGroup = endpoints.MapGroup("users").WithTags("Users");

        usersGroup.MapPost("create", CreateUser)
            .WithName("CreateUser")
            .WithDescription("Create a new User")
            .Accepts<CreateUser>(contentType: MediaTypeNames.Application.Json)
            .WithOpenApi();

        usersGroup.MapGet(GetById, GetUserById)
            .WithName("GetUser")
            .WithDescription("Get a User")
            .WithOpenApi();
    }

    private static async Task<CreatedAtRoute> CreateUser(CreateUser command, IMessageBus bus)
    {
        var created = await bus.InvokeAsync<User>(command);
        return TypedResults.CreatedAtRoute("GetUser", new { id = created.Id });
    }

    private static Results<Ok<User>, NotFound> GetUserById(string id, UserRepository repository)
    {
        if (!Ulid.TryParse(id, out var ulid))
        {
            return TypedResults.NotFound();
        }

        var user = repository.Get(ulid);
        return user is null
            ? TypedResults.NotFound()
            : TypedResults.Ok(user);
    }
}