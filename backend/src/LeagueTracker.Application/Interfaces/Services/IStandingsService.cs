using LeagueTracker.Application.DTOs;

namespace LeagueTracker.Application.Interfaces.Services;

public interface IStandingsService
{
    Task<List<StandingsRowDto>> GetStandingsAsync(int seasonId);
    Task<List<string>> GetTeamFormAsync(int teamId, int seasonId, int lastMatches = 5);
}