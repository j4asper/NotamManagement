namespace NotamManagement.Shared.Models;

public class User
{
    public int Id { get; set; }
    
    public string FullName { get; set; }
    
    public DateTime DateOfBirth { get; set; }
    
    public string Username { get; set; }
    
    public string Password { get; set; }
    
    public int OrganizationId { get; set; }
    public Organization Organization { get; set; }
}