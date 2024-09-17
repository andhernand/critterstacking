namespace Wolvie.Issues.Commands;

public record AssignIssue
{
    public required Ulid IssueId { get; init; }
    public required Ulid AssigneeId { get; init; }
}