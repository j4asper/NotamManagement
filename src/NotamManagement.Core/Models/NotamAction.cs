﻿using System.Text.Json.Serialization;

namespace NotamManagement.Core.Models;

public class NotamAction
{
    public int Id { get; set; }
    
    public required int NotamId { get; set; }

    public required int OrganizationId { get; set; }    
    
    public Importance Importance { get; set; }

    
    public Notam? Notam { get; set; }

    public string? Note { get; set; }
}