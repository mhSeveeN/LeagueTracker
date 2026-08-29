using LeagueTracker.Domain.Enums;

namespace LeagueTracker.Application.DTOs;

public class MatchDto
{
    public int Id { get; set; }
    public int SeasonId { get; set; }
    public int Round { get; set; }
    public DateTime KickOff { get; set; }
    public string? Venue { get; set; }
    public MatchStatus Status { get; set; }

    public int HomeTeamId { get; set; }
    public string HomeTeamName { get; set; } = string.Empty;

    public int AwayTeamId { get; set; }
    public string AwayTeamName { get; set; } = string.Empty;

    public int? HomeScore { get; set; }
    public int? AwayScore { get; set; }
}