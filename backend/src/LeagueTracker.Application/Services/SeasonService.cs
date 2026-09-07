using LeagueTracker.Application.DTOs;
using LeagueTracker.Application.Interfaces.Repositories;
using LeagueTracker.Application.Interfaces.Services;
using LeagueTracker.Application.Mappings;
using LeagueTracker.Domain.Entities;
using LeagueTracker.Domain.Enums;

namespace LeagueTracker.Application.Services;

public class SeasonService : ISeasonService
{
    private readonly ISeasonRepository _seasonRepository;
    private readonly ITeamRepository _teamRepository;
    private readonly IMatchRepository _matchRepository;

    public SeasonService(
        ISeasonRepository seasonRepository,
        ITeamRepository teamRepository,
        IMatchRepository matchRepository)
    {
        _seasonRepository = seasonRepository;
        _teamRepository = teamRepository;
        _matchRepository = matchRepository;
    }

    public async Task<SeasonDto?> GetByIdAsync(int id)
    {
        var season = await _seasonRepository.GetByIdAsync(id);
        return season?.ToDto();
    }

    public async Task<List<SeasonDto>> GetAllAsync()
    {
        var seasons = await _seasonRepository.GetAllAsync();
        return seasons.Select(s => s.ToDto()).ToList();
    }

    public async Task<SeasonDto?> GetActiveAsync()
    {
        var season = await _seasonRepository.GetActiveAsync();
        return season?.ToDto();
    }

    public async Task<SeasonDto> CreateAsync(SeasonDto seasonDto)
    {
        var season = new Season
        {
            Name = seasonDto.Name,
            StartDate = seasonDto.StartDate,
            EndDate = seasonDto.EndDate,
            IsActive = seasonDto.IsActive
        };

        var created = await _seasonRepository.AddAsync(season);
        return created.ToDto();
    }

    public async Task UpdateAsync(SeasonDto seasonDto)
    {
        var season = await _seasonRepository.GetByIdAsync(seasonDto.Id) ?? throw new KeyNotFoundException($"Nie znaleziono sezonu o id {seasonDto.Id}");

        season.Name = seasonDto.Name;
        season.StartDate = seasonDto.StartDate;
        season.EndDate = seasonDto.EndDate;
        season.IsActive = seasonDto.IsActive;

        await _seasonRepository.UpdateAsync(season);
    }

    public async Task DeleteAsync(int id)
    {
        await _seasonRepository.DeleteAsync(id);
    }

    public async Task AssignTeamAsync (int seasonId, int teamId)
    {
        var season = await _seasonRepository.GetByIdWithTeamsAsync(seasonId) ?? throw new KeyNotFoundException($"Nie znaleziono sezonu o id {seasonId}");

        var team = await _teamRepository.GetByIdAsync(teamId) ?? throw new KeyNotFoundException($"Nie znaleziono drużyny o id {teamId}");

        if (season.Teams.Any(t => t.Id == teamId))
            return;

        season.Teams.Add(team);
        await _seasonRepository.UpdateAsync(season);
    }

    public async Task RemoveTeamAsync(int seasonId, int teamId)
    {
        var season = await _seasonRepository.GetByIdWithTeamsAsync(seasonId) ?? throw new KeyNotFoundException($"Nie znaleziono sezonu o id {seasonId}");

        var team = season.Teams.FirstOrDefault(t => t.Id == teamId);
        if (team is not null)
        {
            season.Teams.Remove(team);
            await _seasonRepository.UpdateAsync(season);
        }
    }

    public async Task<List<MatchDto>> GenerateRoundRobinScheduleAsync(int seasonId, DateTime firstRoundDate, TimeSpan matchInterval)
    {
        var season = await _seasonRepository.GetByIdWithTeamsAsync(seasonId) ?? throw new KeyNotFoundException($"Nie znaleziono sezonu o id {seasonId}");

        var teams = season.Teams.ToList();
        if (teams.Count < 2)
            throw new InvalidOperationException("Do wygenerowania terminarza potrzeba co najmniej 2 drużyn.");

        var byeTeam = new Team { Id = -1, Name = "BYE" };
        if (teams.Count % 2 != 0)
            teams.Add(byeTeam);

        var teamCount = teams.Count;
        var roundsPerHalf = teamCount - 1;
        var half = teamCount / 2;

        var firstHalfPairings = new List<List<(Team Home, Team Away)>>();
        var rotation = new List<Team>(teams);

        for (var round = 0; round < roundsPerHalf; round++)
        {
            var roundPairings = new List<(Team Home, Team Away)>();
            for (var i = 0; i < half; i++)
            {
                var home = rotation[i];
                var away = rotation[teamCount - 1 - i];

                if (round % 2 == 1)
                {
                    (home, away) = (away, home);
                }

                if (home.Id != -1 && away.Id != -1)
                {
                    roundPairings.Add((home, away));
                }
            }
            firstHalfPairings.Add(roundPairings);

            var last = rotation[^1];
            rotation.RemoveAt(rotation.Count - 1);
            rotation.Insert(1, last);
        }

        var matchesToCreate = new List<Match>();
        var roundNumber = 1;
        var kickOff = firstRoundDate;

        foreach (var (home, away) in roundPairings)
        {
            matchesToCreate.Add(new Match
            {
                SeasonId = seasonId,
                Round = roundNumber,
                KickOff = kickOff,
                Status = MatchStatus.Scheduled,
                HomeTeamId = home.Id,
                HomeTeam = home,
                AwayTeamId = away.Id,
                AwayTeam = away
            });
        }
        roundNumber++;
        kickOff = kickOff.Add(matchInterval);
    }

    foreach (var roundPairings in firstHalfPairings) 
    {
        foreach (var (home, away) in roundPairings)
        {
            matchesToCreate.Add(new Match
            {
                SeasonId = seasonId,
                Round = roundNumber,
                KickOff = kickOff,
                Status = MatchStatus.Scheduled,
                HomeTeamId = home.Id,
                HomeTeam = home,
                AwayTeamId = away.Id,
                AwayTeam = away
            });
        }
        roundNumber++;
        kickOff = kickOff.Add(matchInterval);
    }

    var created = new List<Match>();
    foreach (var match in matchesToCreate) {
        created.Add(await _matchRepository.AddAsync(match));
    
    return created.Select(m => m.ToDto()).ToList();
    }
}