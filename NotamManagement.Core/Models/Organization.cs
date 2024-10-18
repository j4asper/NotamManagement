namespace NotamManagement.Core.Models;

public class Organization
{
    public int Id { get; set; }
    
    public required string Name { get; set; }
    
    public required List<User> Users { get; set; }
    
    public required List<FlightPlan> FlightPlans { get; set; }
    
    public required List<NotamAction> NotamActions { get; set; }
}