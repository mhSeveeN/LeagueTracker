using LeagueTracker.Domain.Enums;

namespace LeagueTracker.Application.DTOs;

public class CardDto
{
    public int Id { get; set; }
    public int MatchId { get; set; }
    public int PlayerId { get; set; }
    public string PlayerName { get; set; } = string.Empty;
    public int Minute { get; set; }
    public CardType Type { get; set; }
}