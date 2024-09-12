using Serilog;

using Wolvie.Extensions;

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
    }

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}