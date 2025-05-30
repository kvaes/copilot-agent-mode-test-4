using Microsoft.EntityFrameworkCore;
using EventsApi.Models;

namespace EventsApi.Data;

public class EventsDbContext : DbContext
{
    public EventsDbContext(DbContextOptions<EventsDbContext> options) : base(options)
    {
    }

    public DbSet<Event> Events { get; set; } = null!;
    public DbSet<EventRegistration> EventRegistrations { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Configure Event entity
        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Location).IsRequired().HasMaxLength(500);
            entity.Property(e => e.Date).IsRequired();
            entity.Property(e => e.StartTime).IsRequired();
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.Property(e => e.UpdatedAt).IsRequired();
            
            // Configure relationship
            entity.HasMany(e => e.Registrations)
                  .WithOne(r => r.Event)
                  .HasForeignKey(r => r.EventId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Configure EventRegistration entity
        modelBuilder.Entity<EventRegistration>(entity =>
        {
            entity.HasKey(r => r.Id);
            entity.Property(r => r.Name).IsRequired().HasMaxLength(100);
            entity.Property(r => r.Email).IsRequired().HasMaxLength(200);
            entity.Property(r => r.Pronouns).HasMaxLength(50);
            entity.Property(r => r.OptInForCommunication).IsRequired();
            entity.Property(r => r.RegisteredAt).IsRequired();
            
            // Ensure unique email per event
            entity.HasIndex(r => new { r.EventId, r.Email }).IsUnique();
        });
    }
}