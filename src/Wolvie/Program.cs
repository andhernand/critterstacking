using System.Net.Mime;

using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

using Oakton;

using Serilog;

using Swashbuckle.AspNetCore.SwaggerGen;

using Wolverine;

using Wolvie.Commands;
using Wolvie.Extensions;
using Wolvie.Repositories;

Log.Logger = Logging.CreateBootstrapLogger();

Log.Information("Application starting...");

var builder = WebApplication.CreateBuilder(args);
{
    builder.Host.UseSerilog((context, logConfig) =>
        logConfig.ReadFrom.Configuration(context.Configuration));

    builder.Host.UseWolverine();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>>(_ =>
        new ConfigureOptions<SwaggerGenOptions>(options =>
        {
            options.MapType<Ulid>(() => new OpenApiSchema { Type = "string", Format = "string" });
        }));

    builder.Services.AddSingleton<UserRepository>();
    builder.Services.AddSingleton<IssueRepository>();
}

await using var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseSerilogRequestLogging();

    app.MapPost("/issues/create", async (CreateIssue body, IMessageBus bus) =>
        {
            var created = await bus.InvokeAsync<IssueCreated>(body);
            return TypedResults.Text(created.Id.ToString());
        })
        .WithName("CreateIssue")
        .WithDescription("Create a new issue")
        .WithTags("Issue")
        .Accepts<CreateIssue>(contentType: MediaTypeNames.Application.Json)
        .WithOpenApi();

    app.MapPost("/issues/assign", async (AssignIssue body, IMessageBus bus) =>
        {
            await bus.InvokeAsync(body);
            return TypedResults.Ok();
        })
        .WithName("AssignIssue")
        .WithDescription("Assign an Issue to an existing User")
        .WithTags("Issue")
        .Accepts<AssignIssue>(contentType: MediaTypeNames.Application.Json)
        .WithOpenApi();

    app.MapPost("/users/create", async (CreateUser body, IMessageBus bus) =>
        {
            var created = await bus.InvokeAsync<UserCreated>(body);
            return TypedResults.Text(created.Id.ToString());
        })
        .WithName("CreateUser")
        .WithDescription("Create a new user")
        .WithTags("User")
        .Accepts<CreateUser>(contentType: MediaTypeNames.Application.Json)
        .WithOpenApi();
}

return await app.RunOaktonCommands(args);