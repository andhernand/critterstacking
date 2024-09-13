using Wolvie.Commands;
using Wolvie.Models;
using Wolvie.Repositories;

namespace Wolvie.Handlers;

public static class CreateUserHandler
{
    public static UserCreated Handle(CreateUser command, UserRepository repository)
    {
        var user = new User { Email = command.Email, Name = command.Name };
        repository.Store(user);
        return new UserCreated(user.Id);
    }
}