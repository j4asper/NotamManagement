namespace NotamManagement.Shared.Models;

public class Notam
{
    public int Id { get; set; }
    
    public DateTime ValidFrom { get; set; }
    
    public DateTime ValidTo { get; set; }
    
    public DateTime EffectiveValidTo { get; set; }
    
    public NotamType Type { get; set; }
    
    public string NotamOffice { get; set; }
    
    public string QCode { get; set; }
    
    public string Identifier  { get; set; }
    
    public string RefferenceIdentifier  { get; set; }
    
    public string FIR { get; set; }
    
    public string Purpose { get; set; }
    
    public Coordinates Coordinates { get; set; }
}