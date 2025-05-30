using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventsApi.Models;

namespace EventsApi.Services;

public interface IEventService
{
    Task<Event> CreateEventAsync(CreateEventRequest request);
    Task<Event?> GetEventAsync(int id);
    Task<List<Event>> GetEventsAsync(DateTime? date = null, string? location = null);
    Task<Event?> UpdateEventAsync(int id, UpdateEventRequest request);
    Task<bool> DeleteEventAsync(int id);
    Task<EventRegistration> RegisterForEventAsync(int eventId, EventRegistrationRequest request);
}