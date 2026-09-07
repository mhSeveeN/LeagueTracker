using LeagueTracker.Application.DTOs;
using LeagueTracker.Application.Interfaces.Repositories;
using LeagueTracker.Application.Interfaces.Services;
using LeagueTracker.Application.Mappings;
using LeagueTracker.Domain.Entities;
using LeagueTracker.Domain.Enums;

namespace LeagueTracker.Application.Services;

public class MatchService : IMatchService
{
    private readonly IMatchRepository _matchRepository;
    private readonly ITeamRepository _teamRepository;

    public MatchService(IMatchRepository matchRepository, ITeamRepository teamRepository)
    {
        _matchRepository = matchRepository;
        _teamRepository = teamRepository;
    }

    public async Task<MatchDetailDto?> GetByIdAsync(int id)
    {
        var match = await _matchRepository.GetByIdAsync(id);
        return match?.ToMatchDetailDto();
    }

    public async Task<List<MatchDto>> GetBySeasonAsync(int seasonId)
    {
        var matches = await _matchRepository.GetBySeasonAsync(seasonId);
        return matches.Select(m => m.ToDto()).ToList();
    }

    public async Task<MatchDto> CreateAsync(MatchDto matchDto)
    {
        var homeTeam = await _teamRepository.GetByIdAsync(matchDto.HomeTeamId) ?? throw new KeyNotFoundException($"Nie znaleziono drużyny gospodarzy o id {matchDto.HomeTeamId}");
        var awayTeam = await _teamRepository.GetByIdAsync(matchDto.AwayTeamId) ?? throw new KeyNotFoundException($"Nie znaleziono drużyny gości o id {matchDto.AwayTeamId}");

        if (homeTeam.Id == awayTeam.Id)
        {
            throw new ArgumentException("Drużyna nie może grać sama ze sobą.");
        }

        var match = new Match
        {
            SeasonId = matchDto.SeasonId,
            Round = matchDto.Round,
            KickOff = matchDto.KickOff,
            Venue = matchDto.Venue,
            Status = MatchStatus.Scheduled,
            HomeTeamId = homeTeam.Id,
            AwayTeamId = awayTeam.Id,
            AwayTeam = awayTeam,
        };

        var created = await _matchRepository.AddAsync(match);
        return created.ToDto();
    }

    public async Task UpdateResultAsync(int matchId, int homeScore, int awayScore)
    {
        if (homeScore < 0 || awayScore < 0)
        {
            throw new ArgumentException("Wynik nie może być ujemny.");
        }

        var match = await _matchRepository.GetByIdAsync(matchId) ?? throw new KeyNotFoundException($"Nie znaleziono meczu o id {matchId}");

        match.HomeScore = homeScore;
        match.AwayScore = awayScore;
        match.Status = MatchStatus.Finished;

        await _matchRepository.UpdateAsync(match);
    }

    public async Task DeleteAsync(int Id)
    {
        await _matchRepository.DeleteAsync(Id);
    }

    public async Task<GoalDto> AddGoalAsync(int matchId, GoalDto goalDto)
    {
        var match = await _matchRepository.GetByIdAsync(matchId) ?? throw new KeyNotFoundException($"Nie znaleziono meczu o id {matchId}");

        var scorer = await _teamRepository.GetPlayerByIdAsync(goalDto.ScorerId) ?? throw new KeyNotFoundException($"Nie znaleziono zawodnika o id {goalDto.ScorerId}");

        Player? assistBy = null;
        if (goalDto.AssistById.HasValue)
        {
            assistBy = await _teamRepository.GetPlayerByIdAsync(goalDto.AssistById.Value) ?? throw new KeyNotFoundException($"Nie znaleziono zawodnika (asysta) o id {goalDto.AssistById}");
        }

        var goal = new Goal
        {
            MatchId = matchId,
            Match = match,
            ScorerId = scorer.Id,
            Scorer = scorer,
            AssistById = assistBy?.Id,
            AssistBy = assistBy,
            Minute = goalDto.Minute,
            Type = goalDto.Type,
        };

        var created = await _matchRepository.AddGoalAsync(goal);
        return created.ToDto();
    }

    public async Task RemoveGoalAsync(int goalId)
    {
        await _matchRepository.DeleteGoalAsync(goalId);
    }

    public async Task<CardDto> AddCardAsync(int matchId, CardDto cardDto)
    {
        var match = await _matchRepository.GetByIdAsync(matchId) ?? throw new KeyNotFoundException($"Nie znaleziono meczu o id {matchId}");

        var player = await _teamRepository.GetPlayerByIdAsync(cardDto.PlayerId) ?? throw new KeyNotFoundException($"Nie znaleziono zawodnika o id {cardDto.PlayerId}");

        var card = new Card
        {
            MatchId = matchId,
            Match = match,
            PlayerId = player.Id,
            Player = player,
            Minute = cardDto.Minute,
            Type = cardDto.Type,
        };

        var created = await _matchRepository.AddCardAsync(card);
        return created.ToDto();
    }

    public async Task RemoveCardAsync(int cardId)
    {
        await _matchRepository.DeleteCardAsync(cardId);
    }

}