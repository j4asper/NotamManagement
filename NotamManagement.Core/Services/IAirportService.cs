using NotamManagement.Core.Models;

namespace NotamManagement.Core.Services;

public interface IAirportService
{
    Task<IReadOnlyList<Airport>> GetAllAsync();
}