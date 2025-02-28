namespace NotamManagement.Core.Models;

public class Organization
{
    public int Id { get; set; }
    
    public required string Name { get; set; }
    
    public List<User> Users { get; set; } = new List<User>();
    
    public List<FlightPlan> FlightPlans { get; set; } = new List<FlightPlan>();
    
    public List<NotamAction> NotamActions { get; set; } = new List<NotamAction>();
}