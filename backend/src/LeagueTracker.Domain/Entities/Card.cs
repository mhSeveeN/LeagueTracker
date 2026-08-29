using LeagueTracker.Domain.Enums;

namespace LeagueTracker.Domain.Entities;

public class Card : BaseEntity
{
    public int MatchId { get; set; }
    public Match Match { get; set; } = null!;

    public int PlayerId { get; set; }
    public Player Player { get; set; } = null!;

    public int Minute { get; set; }
    public CardType Type { get; set; }
}