using LeagueTracker.Domain.Entities;

namespace LeagueTracker.Application.Interfaces.Repositories;

public interface ISeasonRepository
{
    Task<Season?> GetByIdAsync(int id);
    Task<Season?> GetByIdWithTeamsAsync(int id);
    Task<List<Season>> GetAllAsync();
    Task<Season?> GetActiveAsync();
    Task<Season> AddAsync(Season season);
    Task UpdateAsync(Season season);
    Task DeleteAsync(int id);
}