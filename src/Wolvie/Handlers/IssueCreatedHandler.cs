using Wolvie.Commands;
using Wolvie.Repositories;

namespace Wolvie.Handlers;

public static class IssueCreatedHandler
{
    public static void Handle(IssueCreated created, IssueRepository repository, Serilog.ILogger logger)
    {
        var issue = repository.Get(created.Id);
        logger.Information("{@Issue} created", issue);
    }
}