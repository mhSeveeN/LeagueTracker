using LeagueTracker.Domain.Enums;

namespace LeagueTracker.Domain.Entities;

public class Match : BaseEntity
{
    public int SeasonId { get; set; }
    public Season Season { get; set; } = null!;

    public int RoundId { get; set; }
    public DateTime KickOff { get; set; }
    public string? Venue { get; set; }
    public MatchStatus Status { get; set; } = MatchStatus.Scheduled;

    public int HomeTeamId { get; set; }
    public Team HomeTeam { get; set; } = null!;

    public int AwayTeamId { get; set; }
    public Team AwayTeam { get; set; } = null!;

    public int? HomeScore { get; set; }
    public int? AwayScore { get; set; }

    public ICollection<Goal> Goals { get; set; } = new List<Goal>();
    public ICollection<Card> Cards { get; set; } = new List<Card>();
}