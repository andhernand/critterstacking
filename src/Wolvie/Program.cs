using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

using Oakton;

using Serilog;

using Swashbuckle.AspNetCore.SwaggerGen;

using Wolverine;

using Wolvie.Extensions;
using Wolvie.Issues;
using Wolvie.Repositories;
using Wolvie.Users;

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
    app.MapIssuesEndpoints();
    app.MapUsersEndpoints();
}

return await app.RunOaktonCommands(args);