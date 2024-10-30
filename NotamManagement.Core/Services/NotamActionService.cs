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

        private readonly IConfiguration config;
        private readonly HttpClient httpClient;

        public NotamActionService(IConfiguration config)
        {
            this.config = config;
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(config.GetRequiredSection("ApiBaseUrl").Value ?? "");
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
            return await response.Content.ReadFromJsonAsync<IReadOnlyList<NotamAction>>();
        }
    }
}
