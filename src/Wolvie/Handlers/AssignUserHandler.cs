using Wolvie.Commands;
using Wolvie.Repositories;

namespace Wolvie.Handlers;

public static class AssignUserHandler
{
    public static async Task<IssueAssigned> Handle(
        AssignIssue command,
        IssueRepository issues,
        Serilog.ILogger logger)
    {
        var issue = await issues.GetAsync(command.IssueId);
        issue.AssigneeId = command.AssigneeId;

        logger.Information("Assigning {@Issue}", issue);

        await issues.StoreAsync(issue);
        return new IssueAssigned(issue.Id);
    }
}