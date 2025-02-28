using System.Net.Http.Json;
using NotamManagement.Core.Models;

namespace NotamManagement.Core.Services;

public class AirportService : IAirportService
{
    private readonly HttpClient httpClient;
    
    public AirportService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }
    
    
    public async Task<IReadOnlyList<Airport>> GetAllAsync()
    {
        var response = await httpClient.GetAsync("/api/airport");

        var airports = await response.Content.ReadFromJsonAsync<IReadOnlyList<Airport>>();
        
        return airports ?? [];
    }
}