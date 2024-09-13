using Wolvie.Commands;
using Wolvie.Models;
using Wolvie.Repositories;

namespace Wolvie.Handlers;

public static class CreateIssueHandler
{
    public static async Task<IssueCreated> Handle(
        CreateIssue command,
        IssueRepository repository,
        Serilog.ILogger logger)
    {
        var issue = new Issue
        {
            Title = command.Title,
            Description = command.Description,
            IsOpen = true,
            Opened = DateTimeOffset.Now,
            OriginatorId = command.OriginatorId
        };
        logger.Information("Creating {@Issue}", issue);

        await repository.StoreAsync(issue);
        return new IssueCreated(issue.Id);
    }
}