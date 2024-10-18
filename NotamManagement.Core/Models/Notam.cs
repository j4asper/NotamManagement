namespace NotamManagement.Core.Models;

public class Notam
{
    public required int Id { get; set; }
    
    public DateTime ValidFrom { get; set; }
    
    public DateTime? ValidTo { get; set; }
    
    public DateTime? EffectiveValidTo { get; set; }
    
    public NotamType Type { get; set; }
    
    public required string NotamOffice { get; set; }
    
    public required string QCode { get; set; }
    
    public required string Identifier  { get; set; }
    
<<<<<<< HEAD
    public required string ReferenceIdentifier { get; set; }
=======
    public string? RefferenceIdentifier  { get; set; }
>>>>>>> 24e5755456ea3098fa9b1288d28f2cd01de5000f
    
    public required string FIR { get; set; }
    
    public required string Purpose { get; set; }

    public string? NotamText { get; set; }
    
    public required Coordinates Coordinates { get; set; }
}