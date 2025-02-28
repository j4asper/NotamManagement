using NotamManagement.Core.Models;
using System.Net.Http.Json;

namespace NotamManagement.Core.Services
{
    public class NotamActionService : INotamActionService
    {

        private readonly HttpClient httpClient;

        public NotamActionService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task AddAsync(NotamAction notamAction)
        {
           var response = await httpClient.PostAsJsonAsync("/api/notamaction", notamAction);
           response.EnsureSuccessStatusCode();

        }

        public async Task<IReadOnlyList<NotamAction>> GetAllNotamActionsAsync()
        {
            var response = await httpClient.GetAsync("/api/notamaction");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IReadOnlyList<NotamAction>>() ?? [];
        }

        public async Task<IReadOnlyList<NotamAction>> GetNotamActionsFromLocationAsync(string location)
        {
            var response = await httpClient.GetAsync($"/api/notamaction/find?location={location.ToUpper()}");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<IReadOnlyList<NotamAction>>();
            return result ?? [];
        }

        public async Task UpdateNotamAction(NotamAction notamAction)
        {
            var response = await httpClient.PutAsJsonAsync($"/api/notamaction/id/{notamAction.Id}", notamAction);
            response.EnsureSuccessStatusCode();
           
        }

        public async Task DeleteNotamAction(NotamAction notamAction)
        {
            var response = await httpClient.DeleteAsync($"/api/notamaction/id/{notamAction.Id}");
            
            response.EnsureSuccessStatusCode();
        }
    }
}
