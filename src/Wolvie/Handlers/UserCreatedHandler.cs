using Wolvie.Commands;
using Wolvie.Repositories;

namespace Wolvie.Handlers;

public static class UserCreatedHandler
{
    public static async Task Handle(
        UserCreated created,
        UserRepository repository,
        Serilog.ILogger logger)
    {
        var user = await repository.GetAsync(created.Id);
        logger.Information("{@User} created", user);
    }
}