using LeaguTracker.Domain.Entities;
using LeaguTracker.Domain.Enums;
using LeagueTracker.Application.DTOs;
using LeagueTracker.Domain.Entities;

namespace LeagueTracker.Application.Services;

public static class StandingsCalculator
{
    private const int PointsForWin = 3;
    private const int PointsForDraw = 1;
    private const int PointsForLoss = 0;

    public static List<StandingsRowDto> Calculate(IEnumerable<Team> teams, IEnumerable<Match> matches)
    {
        var finished = matches.Where(m => m.Status == MatchStatus.Finished && m.HomeScore.HasValue && m.AwayScore.HasValue).ToList();

        var rows = teams.ToDictionary(t => t.Id, t => new StandingsRowDto
        {
            TeamId = t.Id,
            TeamName = t.Name
        });

        foreach(var match in finished)
        {
            if (!rows.TryGetValue(match.HomeTeamId, out var home) || !rows.TryGetValue(match.AwayTeamId, out var away))
            {
                continue; // Skip if team not found
            }

            var homeScore = match.HomeScore!.Value;
            var awayScore = match.AwayScore!.Value;

            home.Played++;
            away.Played++;
            home.GoalsFor += homeScore;
            home.GoalsAgainst += awayScore;
            away.GoalsFor += awayScore;
            away.GoalsAgainst += homeScore;

            if (homeScore > awayScore)
            {
                home.Wins++;
                home.Points += PointsForWin;
                away.Losses++;
                away.Points += PointsForLoss;
            }
            else if (homeScore < awayScore)
            {
                away.Wins++;
                away.Points += PointsForWin;
                home.Losses++;
                home.Points += PointsForLoss;
            }
            else
            {
                home.Draws++;
                home.Points += PointsForDraw;
                away.Draws++;
                away.Points += PointsForDraw;
            }
        }

        foreach (var row in rows.Values)
        {
            row.GoalDifference = row.GoalsFor - row.GoalsAgainst;
        }

        return rows.Values
            .OrderByDescending(r => r.Points)
            .ThenByDescending(r => r.GoalDifference)
            .ThenByDescending(r => r.GoalsFor)
            .ToList();
    }
}