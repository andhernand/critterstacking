using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

using Oakton;

using Testcontainers.PostgreSql;

namespace Wolvie.Tests;

public class WolvieFactory : WebApplicationFactory<IWolvieMarker>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _postgresContainer = new PostgreSqlBuilder()
        .WithImage("postgres:16.4")
        .WithDatabase("postgres")
        .WithUsername("postgres")
        .WithPassword("postgres")
        .Build();

    public async Task InitializeAsync()
    {
        OaktonEnvironment.AutoStartHost = true;
        await _postgresContainer.StartAsync();
    }

    public new async Task DisposeAsync()
    {
        await _postgresContainer.StopAsync();
        await base.DisposeAsync();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseSetting("ConnectionStrings:Postgres", _postgresContainer.GetConnectionString());
    }
}