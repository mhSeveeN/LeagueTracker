using LeagueTracker.Application.DTOs;
using LeagueTracker.Application.Interfaces.Repositories;
using LeagueTracker.Application.Interfaces.Services;
using LeagueTracker.Application.Mappings;
using LeagueTracker.Domain.Enums;

namespace LeagueTracker.Application.Services;

public class StatisticsService : IStatisticsService
{
    private readonly ITeamRepository _teamRepository;
    private readonly IMatchRepository _matchRepository;

    public StatisticsService(ITeamRepository teamRepository, IMatchRepository matchRepository)
    {
        _teamRepository = teamRepository;
        _matchRepository = matchRepository;
    }

    public async Task<TeamStatisticsDto> GetTeamStatisticsAsync(int teamId, int seasonId)
    {
        var team = await _teamRepository.GetByIdAsync(teamId)
            ?? throw new KeyNotFoundException($"Nie znaleziono drużyny o id {teamId}");

        var matches = await _matchRepository.GetByTeamAsync(teamId, seasonId);
        var finished = matches
            .Where(m => m.Status == MatchStatus.Finished && m.HomeScore.HasValue && m.AwayScore.HasValue)
            .ToList();

        var standingsRow = StandingsCalculator.Calculate(new[] { team }, finished).Single();

        var cleanSheets = finished.Count(m =>
            (m.HomeTeamId == teamId && m.AwayScore == 0) ||
            (m.AwayTeamId == teamId && m.HomeScore == 0));

        var teamCards = finished
            .SelectMany(m => m.Cards)
            .Where(c => c.Player.TeamId == teamId)
            .ToList();

        var topScorers = finished
            .SelectMany(m => m.Goals)
            .Where(g => g.Scorer.TeamId == teamId && g.Type != GoalType.OwnGoal)
            .GroupBy(g => g.ScorerId)
            .Select(g => new PlayerGoalsDto
            {
                PlayerId = g.Key,
                PlayerName = $"{g.First().Scorer.FirstName} {g.First().Scorer.LastName}",
                TeamName = team.Name,
                Goals = g.Count()
            })
            .OrderByDescending(p => p.Goals)
            .Take(5)
            .ToList();

        return new TeamStatisticsDto
        {
            TeamId = team.Id,
            TeamName = team.Name,
            MatchesPlayed = standingsRow.Played,
            Wins = standingsRow.Wins,
            Draws = standingsRow.Draws,
            Losses = standingsRow.Losses,
            GoalsFor = standingsRow.GoalsFor,
            GoalsAgainst = standingsRow.GoalsAgainst,
            CleanSheets = cleanSheets,
            YellowCards = teamCards.Count(c => c.Type == CardType.Yellow),
            RedCards = teamCards.Count(c => c.Type == CardType.Red),
            Form = TeamFormCalculator.Calculate(teamId, matches, lastNMatches: 5),
            TopScorers = topScorers
        };
    }

    public async Task<LeagueStatisticsDto> GetLeagueStatisticsAsync(int seasonId)
    {
        var matches = await _matchRepository.GetBySeasonAsync(seasonId);
        var finished = matches
            .Where(m => m.Status == MatchStatus.Finished && m.HomeScore.HasValue && m.AwayScore.HasValue)
            .ToList();

        var topScorers = finished
            .SelectMany(m => m.Goals)
            .Where(g => g.Type != GoalType.OwnGoal)
            .GroupBy(g => g.ScorerId)
            .Select(g => new PlayerGoalsDto
            {
                PlayerId = g.Key,
                PlayerName = $"{g.First().Scorer.FirstName} {g.First().Scorer.LastName}",
                TeamName = g.First().Scorer.Team.Name,
                Goals = g.Count()
            })
            .OrderByDescending(p => p.Goals)
            .Take(10)
            .ToList();

        var mostCards = finished
            .SelectMany(m => m.Cards)
            .GroupBy(c => c.PlayerId)
            .Select(g => new PlayerCardsDto
            {
                PlayerId = g.Key,
                PlayerName = $"{g.First().Player.FirstName} {g.First().Player.LastName}",
                TeamName = g.First().Player.Team.Name,
                YellowCards = g.Count(c => c.Type == CardType.Yellow),
                RedCards = g.Count(c => c.Type == CardType.Red)
            })
            .OrderByDescending(p => p.YellowCards + p.RedCards * 3)
            .Take(10)
            .ToList();

        var biggestWin = finished
            .OrderByDescending(m => Math.Abs(m.HomeScore!.Value - m.AwayScore!.Value))
            .ThenByDescending(m => m.HomeScore!.Value + m.AwayScore!.Value)
            .FirstOrDefault();

        return new LeagueStatisticsDto
        {
            TopScorers = topScorers,
            MostCards = mostCards,
            BiggestWin = biggestWin?.ToDto()
        };
    }
}