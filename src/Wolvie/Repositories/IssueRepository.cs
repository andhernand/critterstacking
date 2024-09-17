using Wolvie.Models;

namespace Wolvie.Repositories;

public class IssueRepository
{
    private readonly Dictionary<Ulid, Issue> _issues = new();

    public void Store(Issue issue)
    {
        _issues[issue.Id] = issue;
    }

    public Issue? Get(Ulid id)
    {
        return _issues.TryGetValue(id, out var issue)
            ? issue
            : null;
    }
}