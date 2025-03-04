using System.Net.Http.Json;
using NotamManagement.Core.Models;

namespace NotamManagement.Core.Services;

public class NotamService : INotamService
{
    private readonly HttpClient httpClient;
    
    public NotamService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }
    
    
    public async Task<IReadOnlyList<Notam>> GetAllNotamsAsync()
    {
        var response = await httpClient.GetAsync("/api/notam");
        if(response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<IReadOnlyList<Notam>>();
        }
        
        
        return [];
    }

    public async IAsyncEnumerable<Notam> GetAllNotamsAsAsyncEnumerable()
    {
        var response = await httpClient.GetAsync("/api/notam/Stream");
        
        response.EnsureSuccessStatusCode();
        
        await foreach(var notam in response.Content.ReadFromJsonAsAsyncEnumerable<Notam>())
        {
            if (notam is null)
                continue;

            yield return notam;
        }
    }

    public async Task<Notam> GetNotamAsync(int notamId)
    {
        var response = await httpClient.GetAsync($"/api/Notam/Id/{notamId}");


        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<Notam>();
        }
        return null;
             
    }
}