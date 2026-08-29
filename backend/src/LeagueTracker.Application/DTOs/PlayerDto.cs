using LeagueTracker.Domain.Enums;

namespace LeagueTracker.Application.DTOs;

public class PlayerDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int ShirtNumber { get; set; }
    public PlayerPosition? Position { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public int TeamId { get; set; }
    public string TeamName { get; set; } = string.Empty;
}