using LeagueTracker.Application.DTOs;
using LeagueTracker.Application.Interfaces.Repositories;
using LeagueTracker.Application.Interfaces.Services;

namespace LeagueTracker.Application.Services;

public class StandingService : IStandingService
{
    private readonly ITeamRepository _teamRepository;
    private readonly IMatchRepository _matchRepository;

    public StandingService(ITeamRepository teamRepository, IMatchRepository matchRepository)
    {
        _teamRepository = teamRepository;
        _matchRepository = matchRepository;
    }

    public async Task<List<StandingsRowDto>> GetStandingsAsync(int seasonId)
    {
        var teams = await _teamRepository.GetBySeasonAsync(seasonId);
        var matches = await _matchRepository.GetBySeasonAsync(seasonId);

        var standings = StandingsCalculator.Calculate(teams, matches);

        foreach (var row in standings)
        {
            row.Form = TeamFormCalculator.Calculate(row.TeamId, matches, lastNMatches: 5);
        }

        return standings;
    }

    public async Task<List<string>> GetTeamFormAsync(int teamId, int lastNMatches = 5)
    {
        var matches = await _matchRepository.GetByTeamAsync(teamId, seasonId);
        return TeamFormCalculator.Calculate(teamId, matches, lastNMatches);
    }
}
    