using Microsoft.AspNetCore.Identity;

namespace NotamManagement.Core.Models;

public class User : IdentityUser
{
    
    public string? FullName { get; set; }
    
    public DateTime DateOfBirth { get; set; }
    
    //public int OrganizationId { get; set; }
    
}