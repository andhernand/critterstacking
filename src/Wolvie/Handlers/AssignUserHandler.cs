using Wolvie.Commands;
using Wolvie.Repositories;

namespace Wolvie.Handlers;

public static class AssignUserHandler
{
    public static IssueAssigned Handle(AssignIssue command, IssueRepository issues)
    {
        var issue = issues.Get(command.IssueId);
        issue.AssigneeId = command.AssigneeId;
        issues.Store(issue);
        return new IssueAssigned(issue.Id);
    }
}