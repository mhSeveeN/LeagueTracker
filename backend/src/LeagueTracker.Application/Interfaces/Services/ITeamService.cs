using LeagueTracker.Application.DTOs;

namespace LeagueTracker.Application.Interfaces.Services;

public interface ITeamService
{
    Task<TeamDto?> GetByIdAsync(int id);
    Task<List<TeamDto>> GetAllAsync();
    Task<List<TeamDto>> GetBySeasonAsync(int seasonId);
    Task<TeamDto> CreateAsync(TeamDto team);
    Task UpdateAsync(TeamDto team);
    Task DeleteAsync(int id);

    Task<List<PlayerDto>> GetPlayersAsync(int teamId);
    Task<PlayerDto> AddPlayerAsync(int teamId, PlayerDto player);
    Task UpdatePlayerAsync(PlayerDto player);
    Task DeletePlayerAsync(int playerId);
}