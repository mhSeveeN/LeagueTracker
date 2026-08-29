using LeagueTracker.Domain.Entities;

namespace LeagueTracker.Application.Interfaces.Repositories;

public interface IMatchRepository
{
    Task<Match?> GetByIdAsync(int id);
    Task<Match?> GetByIdWithDetailsAsync(int id);
    Task<List<Match>> GetBySeasonAsync(int seasonId);
    Task<List<Match>> GetByTeamAsync(int teamId, int seasonId);
    Task<Match> AddAsync(Match match);
    Task UpdateAsync(Match match);
    Task DeleteAsync(int id);

    Task<Goal> AddGoalAsync(Goal goal);
    Task DeleteGoalAsync(int goalId);

    Task<Card> AddCardAsync(Card card);
    Task DeleteCardAsync(int cardId);
}