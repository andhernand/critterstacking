using Wolvie.Commands;
using Wolvie.Repositories;

namespace Wolvie.Handlers;

public static class UserAssignedHandler
{
    public static void Handle(
        IssueAssigned assigned,
        IssueRepository issues,
        UserRepository users,
        Serilog.ILogger logger)
    {
        var issue = issues.Get(assigned.Id);
        var user = users.Get(issue.AssigneeId!.Value);
        logger.Information("User {UserId} was assigned Issue {IssueId}", user.Id, issue.Id);
    }
}