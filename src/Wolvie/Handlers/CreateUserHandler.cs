using Wolvie.Commands;
using Wolvie.Models;
using Wolvie.Repositories;

namespace Wolvie.Handlers;

public static class CreateUserHandler
{
    public static async Task<UserCreated> Handle(
        CreateUser command,
        UserRepository repository,
        Serilog.ILogger logger)
    {
        var user = new User { Email = command.Email, Name = command.Name };
        logger.Information("Creating {@User}", user);

        await repository.StoreAsync(user);
        return new UserCreated(user.Id);
    }
}