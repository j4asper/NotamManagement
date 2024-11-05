using NotamManagement.Core.Models;

namespace NotamManagement.Core.Services;

public interface IFlightPlanService
{
    Task<IReadOnlyList<FlightPlan>> GetAllAsync();
}