using Wolvie.Repositories;

namespace Wolvie.Issues;

public static class AssignUserHandler
{
    public static void Handle(AssignIssue command, IssueRepository issues)
    {
        var issue = issues.Get(command.IssueId);
        if (issue is null)
        {
            return;
        }

        issue.AssigneeId = command.AssigneeId;
        issues.Store(issue);
    }
}