namespace Wolvie.Issues;

public record CreateIssue(Ulid OriginatorId, string Title, string Description);