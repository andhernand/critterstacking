using Wolvie.Commands;
using Wolvie.Repositories;

namespace Wolvie.Handlers;

public static class IssueCreatedHandler
{
    public static async Task Handle(
        IssueCreated created,
        IssueRepository repository,
        Serilog.ILogger logger)
    {
        var issue = await repository.GetAsync(created.Id);
        logger.Information("{@Issue} created", issue);
    }
}