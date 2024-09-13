using Wolvie.Commands;
using Wolvie.Repositories;

namespace Wolvie.Handlers;

public static class UserAssignedHandler
{
    public static async Task Handle(
        IssueAssigned assigned,
        UserRepository users,
        IssueRepository issues,
        Serilog.ILogger logger)
    {
        var issue = await issues.GetAsync(assigned.Id);
        var user = await users.GetAsync(issue.AssigneeId!.Value);
        logger.Information("{UserId} was assigned {IssueId}", user.Id, issue.Id);
    }
}