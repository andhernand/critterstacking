using Wolvie.Models;

namespace Wolvie.Repositories;

public class IssueRepository
{
    private readonly Dictionary<Ulid, Issue> _issues = new();

    public Task StoreAsync(Issue issue)
    {
        return Task.FromResult(_issues[issue.Id] = issue);
    }

    public Task<Issue> GetAsync(Ulid id)
    {
        if (_issues.TryGetValue(id, out var issue))
        {
            return Task.FromResult(issue);
        }

        throw new ArgumentOutOfRangeException(nameof(id), "Issue does not exist");
    }
}