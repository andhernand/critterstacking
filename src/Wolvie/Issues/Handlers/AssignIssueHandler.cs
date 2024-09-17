using Wolvie.Issues.Commands;
using Wolvie.Repositories;

namespace Wolvie.Issues.Handlers;

public static class AssignIssueHandler
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