using System.Net.Mime;

using Oakton;

using Serilog;

using Wolverine;

using Wolvie.Commands;
using Wolvie.Extensions;
using Wolvie.Repositories;

Log.Logger = Logging.CreateBootstrapLogger();

try
{
    Log.Information("Application starting...");

    var builder = WebApplication.CreateBuilder(args);
    {
        builder.Host.UseSerilog((context, logConfig) =>
            logConfig.ReadFrom.Configuration(context.Configuration));

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Host.UseWolverine();

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

        app.MapPost("/issues/create", (CreateIssue body, IMessageBus bus) => bus.InvokeAsync(body))
            .WithName("CreateIssue")
            .WithDescription("Create a new issue")
            .WithTags("Issue")
            .Accepts<CreateIssue>(contentType: MediaTypeNames.Application.Json)
            .WithOpenApi();

        app.MapPost("/issues/assign", (AssignIssue body, IMessageBus bus) => bus.InvokeAsync(body))
            .WithName("AssignIssue")
            .WithDescription("Assign an Issue to an existing User")
            .WithTags("Issue")
            .Accepts<AssignIssue>(contentType: MediaTypeNames.Application.Json)
            .WithOpenApi();

        app.MapPost("/users/create", (CreateUser body, IMessageBus bus) => bus.InvokeAsync(body))
            .WithName("CreateUser")
            .WithDescription("Create a new user")
            .WithTags("User")
            .Accepts<CreateUser>(contentType: MediaTypeNames.Application.Json)
            .WithOpenApi();

        app.MapGet("/", () => Results.Redirect("/swagger"));
    }

    await app.RunOaktonCommands(args);
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}