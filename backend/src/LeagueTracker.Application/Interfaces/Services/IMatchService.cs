using LeagueTracker.Application.DTOs;

namespace LeagueTracker.Application.Interfaces.Services;

public interface IMatchService
{
    Task<MatchDetailDto?> GetByIdAsync(int id);
    Task<List<MatchDto>> GetSeasonAsync(int seasonId);
    Task<List<MatchDto>> GetByTeamAsync(int teamId, int seasonId);
    Task<MatchDto> CreateAsync(MatchDto match);
    Task UpdateResultAsync(int matchId, int homeScore, int awayScore);
    Task DeleteAsync(int id);

    Task<GoalDto> AddGoalAsync(int matchId, GoalDto goal);
    Task RemoveGoalAsync(int goalId);

    Task<CardDto> AddCardAsync(int matchId, CardDto card);
    Task RemoveCardAsync(int cardId);
}