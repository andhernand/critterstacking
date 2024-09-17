using Wolvie.Models;
using Wolvie.Repositories;
using Wolvie.Users.Commands;

namespace Wolvie.Users.Handlers;

public static class CreateUserHandler
{
    public static User Handle(CreateUser command, UserRepository repository)
    {
        var user = new User { Email = command.Email, Name = command.Name };
        repository.Store(user);
        return user;
    }
}