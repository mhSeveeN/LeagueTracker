using LeagueTracker.Domain.Entities;
using LeagueTracker.Domain.Enums;

namespace LeagueTracker.Application.Services;

public static class TeamFormCalculator
{
    public static List<string> Calculate(int teamId, IEnumerable<Match> matches, int LastMatches)
    {
        var recentFinished = matches.Where(m => m.Status == MatchStatus.Finished && m.HomeScore.HasValue && m.AwayScore.HasValue && (m.HomeTeamId == teamId || m.AwayTeamId == teamId))
            .OrderByDescending(m => m.KickOff)
            .Take(LastNMatches)
            .OrderBy(m => m.KickOff)
            .ToList();
        
        var form = new List<string>();

        foreach (var match in recentFinished)
        {
            var isHome = match.HomeTeamId == teamId;
            var teamScore = isHome ? match.HomeScore!.Value : match.AwayScore!.Value;
            var opponentScore = isHome ? match.AwayScore!.Value : match.HomeScore!.Value;

            form.Add(teamScore > opponentScore ? "W" : teamScore < opponentScore ? "L" : "D");
        }
            
        return form;
    }
}