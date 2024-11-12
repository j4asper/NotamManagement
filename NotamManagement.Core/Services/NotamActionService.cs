using Microsoft.Extensions.Configuration;
using NotamManagement.Core.Models;
using Npgsql.Replication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

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
    }
}
