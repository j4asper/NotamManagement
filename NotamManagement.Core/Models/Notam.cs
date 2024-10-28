using System.Text.Json.Serialization;

namespace NotamManagement.Core.Models;

public class Notam
{
    public int Id { get; set; }
    
    public required string Location { get; set; }
    
    public DateTime ValidFrom { get; set; }
    
    public DateTime? ValidTo { get; set; }
    
    public DateTime? EffectiveValidTo { get; set; }
    
    public NotamType Type { get; set; }
    
    public required string NotamOffice { get; set; }
    
    public required string QCode { get; set; }
    
    public required string Identifier  { get; set; }
    
    public string? ReferenceIdentifier { get; set; }

    public required string FIR { get; set; }
    
    public required string Purpose { get; set; }

    public required string Traffic { get; set; }

    public required string Scope { get; set; }

    public string? NotamText { get; set; }
    
    [JsonIgnore]
    public bool IsPermanent => ValidTo > new DateTime(9995,12,31);
    
    [JsonIgnore]
    public string? RawNotam => $"{Identifier} NOTAM{Type.ToString().Substring(0, 1)} {ReferenceIdentifier ?? String.Empty} \n" +
        $"Q) {FIR}/Q{QCode}/{Traffic}/{Purpose}/{Scope}/{Coordinates.LowerFlightLevel.ToString("D3")}/{Coordinates.UpperFlightLevel.ToString("D3")}/{Coordinates.LongLat}/{Coordinates.Radius.ToString("D3")} \n" +
        $"A) {NotamOffice}\n" +
        $"B) {ValidFrom.ToString("yyMMddHHmm")} \n{(IsPermanent ? "C) PERM" : ValidTo.HasValue ? $"C) {ValidTo?.ToString("yyMMddHHmm")}" : "")} \n" +
        $"E) {NotamText}";

    public required Coordinates Coordinates { get; set; }
}