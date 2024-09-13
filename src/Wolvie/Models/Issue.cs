namespace Wolvie.Models;

public record Issue
{
    public Ulid Id { get; } = Ulid.NewUlid();
    public Ulid? AssigneeId { get; set; }
    public Ulid? OriginatorId { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public bool IsOpen { get; set; }
    public DateTimeOffset Opened { get; set; }
    public List<IssueTask> Tasks { get; set; } = [];
}