using Wolvie.Models;

namespace Wolvie.Repositories;

public class UserRepository
{
    private readonly Dictionary<Ulid, User> _users = new();

    public Task StoreAsync(User user)
    {
        return Task.FromResult(_users[user.Id] = user);
    }

    public Task<User> GetAsync(Ulid id)
    {
        if (_users.TryGetValue(id, out var user))
        {
            return Task.FromResult(user);
        }

        throw new ArgumentOutOfRangeException(nameof(id), "User does not exist");
    }
}