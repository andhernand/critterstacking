namespace Wolvie.Issues.Commands;

public record CreateIssue
{
    public Ulid OriginatorId { get; init; } = Ulid.NewUlid();
    public required string Title { get; init; } = string.Empty;
    public required string Description { get; init; } = string.Empty;
}