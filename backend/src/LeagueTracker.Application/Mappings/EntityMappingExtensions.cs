using LeagueTracker.Domain.Entities;

namespace LeagueTracker.Application.Mappings;

public static class EntityMappingExtensions
{
    public static SeasonDto ToDto(this Season season) => new()
    {
        Id = season.Id,
        Name = season.Name,
        StartDate = season.StartDate,
        EndDate = season.EndDate,
        IsActive = season.IsActive,
    };

    public static TeamDto ToDto(this Team team) => new()
    {
        Id = team.Id,
        Name = team.Name,
        City = team.City,
        FoundedYear = team.FoundedYear,
        LogoUrl = team.LogoUrl,
    };

    public static PlayerDto ToDto(this Player player) => new()
    {
        Id = player.Id,
        FirstName = player.FirstName,
        LastName = player.LastName,
        ShirtNumber = player.ShirtNumber,
        Position = player.Position,
        DateOfBirth = player.DateOfBirth,
        TeamId = player.TeamId,
        TeamName = player.Team?.Name ?? string.Empty,
    };

    public static MatchDto ToDto(this Match match) => new()
    {
        Id = match.Id,
        SeasonId = match.SeasonId,
        Round = match.Round,
        KickOff = match.KickOff,
        Venue = match.Venue,
        Status = match.Status,
        HomeTeamId = match.HomeTeamId,
        HomeTeamName = match.HomeTeam?.Name ?? string.Empty,
        AwayTeamId = match.AwayTeamId,
        AwayTeamName = match.AwayTeam?.Name ?? string.Empty,
        HomeScore = match.HomeScore,
        AwayScore = match.AwayScore,
    };

    public static MatchDetailDto ToDetailDto(this Match match) => new()
    {
        Id = match.Id,
        SeasonId = match.SeasonId,
        Round = match.Round,
        KickOff = match.KickOff,
        Venue = match.Venue,
        Status = match.Status,
        HomeTeamId = match.HomeTeamId,
        HomeTeamName = match.HomeTeam?.Name ?? string.Empty,
        AwayTeamId = match.AwayTeamId,
        AwayTeamName = match.AwayTeam?.Name ?? string.Empty,
        HomeScore = match.HomeScore,
        AwayScore = match.AwayScore,
        Goals = match.Goals.Select(g => g.ToDto()).ToList(),
        Cards = match.Cards.Select(c => c.ToDto()).ToList(),
    };

    public static GoalDto ToDto(this Goal goal) => new()
    {
        Id = goal.Id,
        MatchId = goal.MatchId,
        ScorerId = goal.ScorerId,
        ScorerName = goal.Scorer is not null ? $"{goal.Scorer.FirstName} {goal.Scorer.LastName}" : string.Empty,
        AssistById = goal.AssistById,
        AssistByName = goal.AssistBy is not null ? $"{goal.AssistBy.FirstName} {goal.AssistBy.LastName}" : null,
        Minute = goal.Minute,
        Type = goal.Type,
    };
    
    public static CardDto ToDto(this Card card) => new()
    {
        Id = card.Id,
        MatchId = card.MatchId,
        PlayerId = card.PlayerId,
        PlayerName = card.Player is not null ? $"{card.Player.FirstName} {card.Player.LastName}" : string.Empty,
        Minute = card.Minute,
        Type = card.Type,
    };
}