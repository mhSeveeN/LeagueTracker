namespace LeagueTracker.Application.DTOs;

public class PlayerGoalsDto
{
    public int PlayerId { get; set; }
    public string PlayerName { get; set; } = string.Empty;
    public string TeamName { get; set; } = string.Empty;
    public int Goals { get; set; }
}