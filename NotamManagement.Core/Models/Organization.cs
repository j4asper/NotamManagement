namespace NotamManagement.Shared.Models;

public class Organization
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public List<User> Users { get; set; }
    
    public List<FlightPlan> FlightPlans { get; set; }
    
    public List<NotamAction> NotamActions { get; set; }
}