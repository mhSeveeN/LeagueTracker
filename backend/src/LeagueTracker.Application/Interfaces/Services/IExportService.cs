namespace LeagueTracker.Application.Interfaces.Services;

public interface IExportService
{
    Task<byte[]> ExportSeasonToPdfAsync(int seasonId);
    Task<byte[]> ExportSeasonToExcelAsync(int seasonId);
}