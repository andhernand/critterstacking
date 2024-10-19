using Oakton;

using Serilog;

using Wolverine;

using Wolvie;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Host.UseSerilog((context, logConfig) =>
        logConfig.ReadFrom.Configuration(context.Configuration));

    builder.Host.UseWolverine(options =>
        options.ApplicationAssembly = typeof(IWolvieMarker).Assembly);

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
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
}

return await app.RunOaktonCommands(args);