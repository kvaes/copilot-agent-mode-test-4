using System;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using EventsApi.Models;
using EventsApi.Services;

namespace EventsApi.Functions;

public class EventsFunctions
{
    private readonly ILogger _logger;
    private readonly IEventService _eventService;

    public EventsFunctions(ILoggerFactory loggerFactory, IEventService eventService)
    {
        _logger = loggerFactory.CreateLogger<EventsFunctions>();
        _eventService = eventService;
    }

    [Function("CreateEvent")]
    public async Task<HttpResponseData> CreateEvent(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "events")] HttpRequestData req)
    {
        _logger.LogInformation("Creating a new event");

        try
        {
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var createRequest = JsonSerializer.Deserialize<CreateEventRequest>(requestBody, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (createRequest == null)
            {
                var badRequestResponse = req.CreateResponse(HttpStatusCode.BadRequest);
                await badRequestResponse.WriteStringAsync("Invalid request body");
                return badRequestResponse;
            }

            var eventEntity = await _eventService.CreateEventAsync(createRequest);

            var response = req.CreateResponse(HttpStatusCode.Created);
            await response.WriteAsJsonAsync(eventEntity);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating event");
            var errorResponse = req.CreateResponse(HttpStatusCode.InternalServerError);
            await errorResponse.WriteStringAsync("An error occurred while creating the event");
            return errorResponse;
        }
    }

    [Function("GetEvent")]
    public async Task<HttpResponseData> GetEvent(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "events/{id:int}")] HttpRequestData req,
        int id)
    {
        _logger.LogInformation($"Getting event with ID: {id}");

        try
        {
            var eventEntity = await _eventService.GetEventAsync(id);

            if (eventEntity == null)
            {
                var notFoundResponse = req.CreateResponse(HttpStatusCode.NotFound);
                await notFoundResponse.WriteStringAsync("Event not found");
                return notFoundResponse;
            }

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(eventEntity);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting event {id}");
            var errorResponse = req.CreateResponse(HttpStatusCode.InternalServerError);
            await errorResponse.WriteStringAsync("An error occurred while retrieving the event");
            return errorResponse;
        }
    }

    [Function("GetEvents")]
    public async Task<HttpResponseData> GetEvents(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "events")] HttpRequestData req)
    {
        _logger.LogInformation("Getting events");

        try
        {
            var query = System.Web.HttpUtility.ParseQueryString(req.Url.Query);
            
            DateTime? date = null;
            if (DateTime.TryParse(query["date"], out var parsedDate))
            {
                date = parsedDate;
            }

            var location = query["location"];

            var events = await _eventService.GetEventsAsync(date, location);

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(events);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting events");
            var errorResponse = req.CreateResponse(HttpStatusCode.InternalServerError);
            await errorResponse.WriteStringAsync("An error occurred while retrieving events");
            return errorResponse;
        }
    }

    [Function("UpdateEvent")]
    public async Task<HttpResponseData> UpdateEvent(
        [HttpTrigger(AuthorizationLevel.Function, "put", Route = "events/{id:int}")] HttpRequestData req,
        int id)
    {
        _logger.LogInformation($"Updating event with ID: {id}");

        try
        {
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var updateRequest = JsonSerializer.Deserialize<UpdateEventRequest>(requestBody, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (updateRequest == null)
            {
                var badRequestResponse = req.CreateResponse(HttpStatusCode.BadRequest);
                await badRequestResponse.WriteStringAsync("Invalid request body");
                return badRequestResponse;
            }

            var eventEntity = await _eventService.UpdateEventAsync(id, updateRequest);

            if (eventEntity == null)
            {
                var notFoundResponse = req.CreateResponse(HttpStatusCode.NotFound);
                await notFoundResponse.WriteStringAsync("Event not found");
                return notFoundResponse;
            }

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(eventEntity);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error updating event {id}");
            var errorResponse = req.CreateResponse(HttpStatusCode.InternalServerError);
            await errorResponse.WriteStringAsync("An error occurred while updating the event");
            return errorResponse;
        }
    }

    [Function("DeleteEvent")]
    public async Task<HttpResponseData> DeleteEvent(
        [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "events/{id:int}")] HttpRequestData req,
        int id)
    {
        _logger.LogInformation($"Deleting event with ID: {id}");

        try
        {
            var success = await _eventService.DeleteEventAsync(id);

            if (!success)
            {
                var notFoundResponse = req.CreateResponse(HttpStatusCode.NotFound);
                await notFoundResponse.WriteStringAsync("Event not found");
                return notFoundResponse;
            }

            var response = req.CreateResponse(HttpStatusCode.NoContent);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error deleting event {id}");
            var errorResponse = req.CreateResponse(HttpStatusCode.InternalServerError);
            await errorResponse.WriteStringAsync("An error occurred while deleting the event");
            return errorResponse;
        }
    }

    [Function("RegisterForEvent")]
    public async Task<HttpResponseData> RegisterForEvent(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "events/{id:int}/register")] HttpRequestData req,
        int id)
    {
        _logger.LogInformation($"Registering for event with ID: {id}");

        try
        {
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var registrationRequest = JsonSerializer.Deserialize<EventRegistrationRequest>(requestBody, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (registrationRequest == null)
            {
                var badRequestResponse = req.CreateResponse(HttpStatusCode.BadRequest);
                await badRequestResponse.WriteStringAsync("Invalid request body");
                return badRequestResponse;
            }

            var registration = await _eventService.RegisterForEventAsync(id, registrationRequest);

            var response = req.CreateResponse(HttpStatusCode.Created);
            await response.WriteAsJsonAsync(registration);
            return response;
        }
        catch (ArgumentException)
        {
            var notFoundResponse = req.CreateResponse(HttpStatusCode.NotFound);
            await notFoundResponse.WriteStringAsync("Event not found");
            return notFoundResponse;
        }
        catch (InvalidOperationException ex)
        {
            var conflictResponse = req.CreateResponse(HttpStatusCode.Conflict);
            await conflictResponse.WriteStringAsync(ex.Message);
            return conflictResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error registering for event {id}");
            var errorResponse = req.CreateResponse(HttpStatusCode.InternalServerError);
            await errorResponse.WriteStringAsync("An error occurred while registering for the event");
            return errorResponse;
        }
    }
}