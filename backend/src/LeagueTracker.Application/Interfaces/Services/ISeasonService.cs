using LeagueTracker.Application.DTOs;

namespace LeagueTracker.Application.Interfaces.Services;

public interface ISeasonService
{
    Task<SeasonDto?> GetByIdAsync(int id);
    Task<List<SeasonDto>> GetAllAsync();
    Task<SeasonDto?> GetActiveAsync();
    Task<SeasonDto> CreateAsync(SeasonDto seasonDto);
    Task UpdateAsync(SeasonDto season);
    Task DeleteAsync(int id);

    Task AssignTeamAsync(int seasonId, int teamId);
    Task RemoveTeamAsync(int seasonId, int teamId);
    Task<List<MatchDto>> GenerateRoundRobinScheduleAsync(int seasonID, DateTime firstRoundDate, TimeSpan matchInterval);
}