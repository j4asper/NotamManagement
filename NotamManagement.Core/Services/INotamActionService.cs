using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotamManagement.Core.Models;

namespace NotamManagement.Core.Services
{
    public interface INotamActionService
    {
        Task<IReadOnlyList<NotamAction>> GetAllNotamActionsAsync();
        Task AddAsync(NotamAction notamAction);
    }
}
