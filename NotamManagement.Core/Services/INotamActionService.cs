using NotamManagement.Core.Models;

namespace NotamManagement.Core.Services
{
    public interface INotamActionService
    {
        Task<IReadOnlyList<NotamAction>> GetAllNotamActionsAsync();
        Task AddAsync(NotamAction notamAction);

        Task<IReadOnlyList<NotamAction>> GetNotamActionsFromLocationAsync(string location);

        Task UpdateNotamAction(NotamAction notamAction);
        
        Task DeleteNotamAction(NotamAction notamAction);
    }
}
