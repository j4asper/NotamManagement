namespace NotamManagement.Shared.Models;

public class NotamAction
{
    public int Id { get; set; }
    
    public int CustomerId { get; set; }
    
    public Notam Notam { get; set; }
    
    public Importance Importance { get; set; }
    
    public string Note { get; set; }
}