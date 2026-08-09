namespace LeagueTracker.Application.DTOs;

public class TeamStatisticsDto
{
    public int TeamId { get; set; }
    public string TeamName { get; set; } = string.Empty;
    public int MatchesPlayed { get; set; }
    public int Wins { get; set; }
    public int Draws { get; set; }
    public int Losses { get; set; }
    public int GoalsFor { get; set; }
    public int GoalsAgainst { get; set; }
    public int CleanSheets { get; set; }

    public int YellowCards { get; set; }
    public int RedCards { get; set; }

    public List<string> Form { get; set; } = new();
    public List<PlayerGoalsDto> TopScorers { get; set; } = new();
}