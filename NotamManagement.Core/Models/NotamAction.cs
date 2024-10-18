namespace NotamManagement.Core.Models;

public class NotamAction
{
    public int Id { get; set; }
    
    public int CustomerId { get; set; }
    
    public required Notam Notam { get; set; }
    
    public Importance Importance { get; set; }
    
    public required string Note { get; set; }
}