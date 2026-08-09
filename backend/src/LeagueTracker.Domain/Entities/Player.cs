using LeagueTracker.Domain.Enums;

namespace LeagueTracker.Domain.Entities;

public class Player : BaseEntity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int? ShirtNumber { get; set; }
    public PlayerPosition? Position { get; set; }
    public DateTime? DateOfBirth { get; set; }

    public int TeamId { get; set; }
    public Team Team { get; set; } = null!;

    public ICollection<Goal> Goals { get; set; } = new List<Goal>();
    public ICollection<Card> Cards { get; set; } = new List<Card>();
}