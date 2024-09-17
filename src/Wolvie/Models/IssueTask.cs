namespace Wolvie.Models;

public record IssueTask
{
    public Ulid Id { get; set; } = Ulid.NewUlid();
    public required string Title { get; init; }
    public required string Description { get; init; }
    public DateTimeOffset? Started { get; set; }
    public DateTimeOffset? Finished { get; set; }
}