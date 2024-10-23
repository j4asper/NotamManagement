using NotamManagement.Core.Models;

namespace NotamManagement.Core.Services;

public interface INotamService
{
    Task<IReadOnlyList<Notam>> GetAllNotamsAsync();
}