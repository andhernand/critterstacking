namespace Wolvie.Commands;

public record CreateIssue(Ulid OriginatorId, string Title, string Description);