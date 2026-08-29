namespace LeagueTracker.Application.DTOs;

public class TeamDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? City { get; set; }
    public string? FoundedYear { get; set; }
    public string? LogoUrl { get; set; }
}