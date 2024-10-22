using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;
using NotamManagement.Core.Models;

namespace NotamManagement.Core.Services;

public class NotamService : INotamService
{
    private readonly IConfiguration config;
    private readonly HttpClient httpClient;

    public NotamService(IConfiguration config)
    {
        this.config = config;
        httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(config.GetRequiredSection("ApiBaseUrl").Value ?? "");
    }
    
    
    public async Task<IReadOnlyList<Notam>> GetAllNotamsAsync()
    {
        var response = await httpClient.GetAsync("/api/notam");

        var notams = await response.Content.ReadFromJsonAsync<IReadOnlyList<Notam>>();
        
        return notams ?? [];
    }
}