namespace Wolvie.Models;

public record User
{
    public Ulid Id { get; set; } = Ulid.NewUlid();
    public required string Email { get; init; }
    public required string Name { get; init; }
}