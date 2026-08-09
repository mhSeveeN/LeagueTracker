namespace LeagueTracker.Application.DTOs;

public class PlayerCardsDto
{
    public int PlayerId { get; set; }
    public string PlayerName { get; set; } = string.Empty;
    public string TeamName { get; set; } = string.Empty;
    public int YellowCards { get; set; }
    public int RedCards { get; set; }
}