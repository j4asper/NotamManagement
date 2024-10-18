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
    
    public required string ReferenceIdentifier { get; set; }
    
    public required string FIR { get; set; }
    
    public required string Purpose { get; set; }
    
    public required Coordinates Coordinates { get; set; }
}