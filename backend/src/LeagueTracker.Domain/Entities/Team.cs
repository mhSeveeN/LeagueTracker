namespace LeagueTracker.Domain.Entities;

public class Team : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? City { get; set; }
    public int? FoundedYear { get; set; }
    public string? LogoUrl { get; set; }

    public ICollection<Season> Seasons { get; set; } = new List<Season>();
    public ICollection<Player> Players { get; set; } = new List<Player>();
    public ICollection<Match> HomeMatches { get; set; } = new List<Match>();
    public ICollection<Match> AwayMatches { get; set; } = new List<Match>();
}