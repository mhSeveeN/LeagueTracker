using LeagueTracker.Application.DTOs;

namespace LeagueTracker.Application.Interfaces.Services;

public interface IStatisticsService
{
    Task<TeamStatisticsDto> GetTeamStatisticsAsync(int teamId, int seasonId);
    Task<LeagueStatisticsDto> GetLeagueStatisticsAsync(int seasonId);
}