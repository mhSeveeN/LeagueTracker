using LeagueTracker.Domain.Enums;

namespace LeagueTracker.Application.DTOs;

public class GoalDto
{
    public int Id { get; set; }
    public int MatchId { get; set; }
    public int ScorerId { get; set; }
    public string ScorerName { get; set; } = string.Empty;
    public int? AssistById { get; set; }
    public string? AssistByName { get; set; }
    public int Minute { get; set; }
    public GoalType Type { get; set; }
}