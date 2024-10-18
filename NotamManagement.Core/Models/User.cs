namespace NotamManagement.Core.Models;

public class User
{
    public required int Id { get; set; }
    
    public required string FullName { get; set; }
    
    public DateTime DateOfBirth { get; set; }
    
    public required string Username { get; set; }
    
    public required string Password { get; set; }
    
    public int OrganizationId { get; set; }
    
    public required Organization Organization { get; set; }
}