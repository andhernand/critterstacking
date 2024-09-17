using Wolvie.Models;

namespace Wolvie.Repositories;

public class UserRepository
{
    private readonly Dictionary<Ulid, User> _users = new();

    public void Store(User user)
    {
        _users[user.Id] = user;
    }

    public User? Get(Ulid id)
    {
        return _users.TryGetValue(id, out var user)
            ? user
            : null;
    }
}