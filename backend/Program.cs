using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using EventsApi.Data;
using EventsApi.Services;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        
        // Add Entity Framework with in-memory database for simplicity
        services.AddDbContext<EventsDbContext>(options =>
            options.UseInMemoryDatabase("EventsDb"));
        
        // Add services
        services.AddScoped<IEventService, EventService>();
    })
    .Build();

host.Run();