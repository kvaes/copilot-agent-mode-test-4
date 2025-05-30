using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventsApi.Models;

public class Event
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [StringLength(500)]
    public string Location { get; set; } = string.Empty;
    
    [Required]
    public DateTime Date { get; set; }
    
    [Required]
    public TimeSpan StartTime { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    // Navigation property for registrations
    public List<EventRegistration> Registrations { get; set; } = new();
}

public class EventRegistration
{
    public int Id { get; set; }
    public int EventId { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [EmailAddress]
    [StringLength(200)]
    public string Email { get; set; } = string.Empty;
    
    [StringLength(50)]
    public string Pronouns { get; set; } = string.Empty;
    
    public bool OptInForCommunication { get; set; }
    
    public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;
    
    // Navigation property
    public Event Event { get; set; } = null!;
}

public class CreateEventRequest
{
    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [StringLength(500)]
    public string Location { get; set; } = string.Empty;
    
    [Required]
    public DateTime Date { get; set; }
    
    [Required]
    public TimeSpan StartTime { get; set; }
}

public class UpdateEventRequest
{
    [StringLength(200)]
    public string? Name { get; set; }
    
    [StringLength(500)]
    public string? Location { get; set; }
    
    public DateTime? Date { get; set; }
    
    public TimeSpan? StartTime { get; set; }
}

public class EventRegistrationRequest
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [EmailAddress]
    [StringLength(200)]
    public string Email { get; set; } = string.Empty;
    
    [StringLength(50)]
    public string Pronouns { get; set; } = string.Empty;
    
    public bool OptInForCommunication { get; set; }
}