namespace LeagueTracker.Application.DTOs;

public class LeagueStatisticsDto
{
    public List<PlayerGoalsDto> TopScorers { get; set; } = new();
    public List<PlayercardsDto> MostCards { get; set; } = new();
    public MatchDto? BiggestWin { get; set; }
}
