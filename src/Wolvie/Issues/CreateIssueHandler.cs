using Wolvie.Models;
using Wolvie.Repositories;

namespace Wolvie.Issues;

public static class CreateIssueHandler
{
    public static Issue Handle(CreateIssue command, IssueRepository repository)
    {
        var issue = new Issue
        {
            Title = command.Title,
            Description = command.Description,
            IsOpen = true,
            Opened = DateTimeOffset.Now,
            OriginatorId = command.OriginatorId
        };

        repository.Store(issue);
        return issue;
    }
}