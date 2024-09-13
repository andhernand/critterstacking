using Wolvie.Commands;
using Wolvie.Models;
using Wolvie.Repositories;

namespace Wolvie.Handlers;

public static class CreateIssueHandler
{
    public static IssueCreated Handle(CreateIssue command, IssueRepository repository)
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
        return new IssueCreated(issue.Id);
    }
}