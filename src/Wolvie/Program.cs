using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

using Serilog;

using Swashbuckle.AspNetCore.SwaggerGen;

using Wolverine;

using Wolvie;
using Wolvie.Issues;
using Wolvie.Repositories;
using Wolvie.Users;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Host.UseSerilog((context, logConfig) =>
        logConfig.ReadFrom.Configuration(context.Configuration));

    builder.Host.UseWolverine(options =>
        options.ApplicationAssembly = typeof(IWolvieMarker).Assembly);

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

var app = builder.Build();
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

app.Run();