﻿namespace NotamManagement.Core.Models;

public class Airport
{
    public required int Id { get; set; }
    
    public required string ICAO { get; set; }
    
    public required string FIR { get; set; }
}