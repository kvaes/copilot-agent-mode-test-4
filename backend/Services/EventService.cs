using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EventsApi.Data;
using EventsApi.Models;

namespace EventsApi.Services;

public class EventService : IEventService
{
    private readonly EventsDbContext _context;

    public EventService(EventsDbContext context)
    {
        _context = context;
    }

    public async Task<Event> CreateEventAsync(CreateEventRequest request)
    {
        var eventEntity = new Event
        {
            Name = request.Name,
            Location = request.Location,
            Date = request.Date,
            StartTime = request.StartTime,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Events.Add(eventEntity);
        await _context.SaveChangesAsync();
        return eventEntity;
    }

    public async Task<Event?> GetEventAsync(int id)
    {
        return await _context.Events
            .Include(e => e.Registrations)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<List<Event>> GetEventsAsync(DateTime? date = null, string? location = null)
    {
        var query = _context.Events.Include(e => e.Registrations).AsQueryable();

        if (date.HasValue)
        {
            query = query.Where(e => e.Date.Date == date.Value.Date);
        }

        if (!string.IsNullOrEmpty(location))
        {
            query = query.Where(e => e.Location.Contains(location));
        }

        return await query.OrderBy(e => e.Date).ThenBy(e => e.StartTime).ToListAsync();
    }

    public async Task<Event?> UpdateEventAsync(int id, UpdateEventRequest request)
    {
        var eventEntity = await _context.Events.FindAsync(id);
        if (eventEntity == null)
            return null;

        if (!string.IsNullOrEmpty(request.Name))
            eventEntity.Name = request.Name;

        if (!string.IsNullOrEmpty(request.Location))
            eventEntity.Location = request.Location;

        if (request.Date.HasValue)
            eventEntity.Date = request.Date.Value;

        if (request.StartTime.HasValue)
            eventEntity.StartTime = request.StartTime.Value;

        eventEntity.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return eventEntity;
    }

    public async Task<bool> DeleteEventAsync(int id)
    {
        var eventEntity = await _context.Events.FindAsync(id);
        if (eventEntity == null)
            return false;

        _context.Events.Remove(eventEntity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<EventRegistration> RegisterForEventAsync(int eventId, EventRegistrationRequest request)
    {
        var eventEntity = await _context.Events.FindAsync(eventId);
        if (eventEntity == null)
            throw new ArgumentException("Event not found", nameof(eventId));

        // Check if already registered
        var existingRegistration = await _context.EventRegistrations
            .FirstOrDefaultAsync(r => r.EventId == eventId && r.Email == request.Email);
        
        if (existingRegistration != null)
            throw new InvalidOperationException("Email already registered for this event");

        var registration = new EventRegistration
        {
            EventId = eventId,
            Name = request.Name,
            Email = request.Email,
            Pronouns = request.Pronouns,
            OptInForCommunication = request.OptInForCommunication,
            RegisteredAt = DateTime.UtcNow
        };

        _context.EventRegistrations.Add(registration);
        await _context.SaveChangesAsync();
        return registration;
    }
}