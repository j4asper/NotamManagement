using NotamManagement.Core.Models;

namespace NotamManagement.Core.Services;

public interface IFlightPlanService
{
    Task<IReadOnlyList<FlightPlan>> GetAllAsync();
    Task AddAsync(FlightPlan flightPlan);
    Task<FlightPlan> GetAsync(int flightPlanId);
}