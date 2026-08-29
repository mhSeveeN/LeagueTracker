using LeagueTracker.Domain.Entities;

namespace LeagueTracker.Application.Interfaces.Repositories;

public interface ITeamRepository
{
    Task<Team?> GetByIdAsync(int id);
    Task<Team?> GetByIdWithPlayerAsync(int id);
    Task<List<Team>> GetAllAsync();
    Task<List<Team>> GetBySeasonAsync(int seasonId);
    Task<Team> AddAsync(Team team);
    Task UpdateAsync(Team team);
    Task DeleteAsync(int id);

    Task<Player?> GetPlayerByIdAsync(int playerId);
    Task<Player> AddPlayerAsync(Player player);
    Task UpdatePlayerAsync(Player player);
    Task DeletePlayerAsync(int playerId);
}