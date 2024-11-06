using System.Net.Http.Json;
using NotamManagement.Core.Models;

namespace NotamManagement.Core.Services;

public class FlightPlanService : IFlightPlanService
{
    private readonly HttpClient httpClient;
    
    public FlightPlanService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }
    
    
    public async Task<IReadOnlyList<FlightPlan>> GetAllAsync()
    {
        var response = await httpClient.GetAsync("/api/flightplan");

        var flightPlans = await response.Content.ReadFromJsonAsync<IReadOnlyList<FlightPlan>>();
        
        return flightPlans ?? [];
    }

    public async Task AddAsync(FlightPlan flightPlan)
    {
        var response = await httpClient.PostAsJsonAsync("/api/flightplan", flightPlan);

        response.EnsureSuccessStatusCode();
    }
}