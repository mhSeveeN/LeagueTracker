using LeagueTracker.Application.DTOs;
using LeagueTracker.Application.Interfaces.Repositories;
using LeagueTracker.Application.Interfaces.Services;
using LeagueTracker.Application.Mappings;
using LeagueTracker.Domain.Entities;

namespace LeagueTracker.Application.Services;

public class TeamService : ITeamService
{
    private readonly ITeamRepository _teamRepository;

    public TeamService(ITeamRepository teamRepository)
    {
        _teamRepository = teamRepository;
    }

    public async Task<TeamDto?> GetByIdAsync(int id)
    {
        var team = await _teamRepository.GetByIdAsync(id);
        return team?.ToDto();
    }

    public async Task<List<TeamDto>> GetAllAsync()
    {
        var teams = await _teamRepository.GetAllAsync();
        return teams.Select(t => t.ToDto()).ToList();
    }
    
    public async Task<List<TeamDto>> GetBySeasonAsync(int seasonId)
    {
        var teams = await _teamRepository.GetBySeasonAsync(seasonId);
        return teams.Select(t => t.ToDto()).ToList();
    }

    public async Task<TeamDto> CreateAsync(TeamDto teamDto)
    {
        var team = new Team
        {
            Name = teamDto.Name,
            City = teamDto.City,
            FoundedYear = teamDto.FoundedYear,
            LogoUrl = teamDto.LogoUrl
        };

        var created = await _teamRepository.AddAsync(team);
        return created.ToDto();
    }

    public async Task UpdateAsync(TeamDto teamDto)
    {
        var team = await _teamRepository.GetByIdAsync(teamDto.Id) 
                   ?? throw new KeyNotFoundException($"Nie znalezniono drużyny o ID {teamDto.Id} ");

        team.Name = teamDto.Name;
        team.City = teamDto.City;
        team.FoundedYear = teamDto.FoundedYear;
        team.LogoUrl = teamDto.LogoUrl;

        await _teamRepository.UpdateAsync(team);        
    }

    public async Task DeleteAsync(int id)
    {
        await _teamRepository.DeleteAsync(id);
    }

    public async Task<List<PlayerDto>> GetPlayersAsync(int teamId)
    {
        var team = await _teamRepository.GetByIdAsync(teamId) 
                   ?? throw new KeyNotFoundException($"Nie znalezniono drużyny o ID {teamId} ");
        
        return team.Players.Select(p => p.ToDto()).ToList();
    }

    public async Task<PlayerDto> AddPlayerAsync(int teamId, PlayerDto playerDto)
    {
        var team = await _teamRepository.GetByIdAsync(teamId) 
                   ?? throw new KeyNotFoundException($"Nie znalezniono drużyny o ID {teamId} ");

        var player = new Player
        {
            FirstName = playerDto.FirstName,
            LastName = playerDto.LastName,
            ShirtNumber = playerDto.ShirtNumber,
            Position = playerDto.Position,
            DateOfBirth = playerDto.DateOfBirth,
            TeamId = teamId,
            Team = team
        };

        var created = await _teamRepository.AddPlayerAsync(player);
        return created.ToDto();
    }

    public async Task UpdatePlayerAsync(PlayerDto playerDto)
    {
        var player = await _teamRepository.GetPlayerByIdAsync(playerDto.Id) 
                     ?? throw new KeyNotFoundException($"Nie znalezniono zawodnika o ID {playerDto.Id} ");

        player.FirstName = playerDto.FirstName;
        player.LastName = playerDto.LastName;
        player.ShirtNumber = playerDto.ShirtNumber;
        player.Position = playerDto.Position;
        player.DateOfBirth = playerDto.DateOfBirth;

        await _teamRepository.UpdatePlayerAsync(player);
    }

    public async Task DeletePlayerAsync(int playerId)
    {
        await _teamRepository.DeletePlayerAsync(playerId);
    }
}