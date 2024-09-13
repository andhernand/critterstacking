using Wolvie.Commands;
using Wolvie.Repositories;

namespace Wolvie.Handlers;

public static class UserCreatedHandler
{
    public static void Handle(
        UserCreated created,
        UserRepository repository,
        Serilog.ILogger logger)
    {
        var user = repository.Get(created.Id);
        logger.Information("{@User} created", user);
    }
}