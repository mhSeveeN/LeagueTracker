namespace LeagueTracker.Application.DTOs;

public class StandingsRowDto
{
    public int TeamId { get; set; }
    public string TeamName { get; set; } = string.Empty;
    public int Played { get; set; }
    public int Wins { get; set; }
    public int Draws { get; set; }
    public int Losses { get; set; }
    public int GoalsFor { get; set; }
    public int GoalsAgainst { get; set; }
    public int GoalDifference { get; set; }
    public int Points { get; set; }

    // "W" / "D" / "L", oldest -> newest
    public List<string> Form { get; set; } = new();
}