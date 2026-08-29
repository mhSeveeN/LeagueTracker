namespace LeagueTracker.Application.DTOs;

public class MatchDetailDto : MatchDto
{
    public List<GoalDto> Goals { get; set; } = new();
    public List<CardDto> Cards { get; set; } = new();
}