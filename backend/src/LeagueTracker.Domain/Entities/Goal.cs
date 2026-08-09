using LeagueTracker.Domain.Enums;

namespace LeagueTracker.Domain.Entities;

public class Goal : BaseEntity
{
    public int MatchId { get; set; }
    public Match Match { get; set; } = null!;

    public int ScorerId { get; set; }
    public Player Scorer { get; set; } = null!;

    public int? AssistById { get; set; }
    public Player? AssistBy { get; set; }

    public int Minute { get; set; }
    public GoalType GoalType { get; set; } = GoalType.Regular;
}