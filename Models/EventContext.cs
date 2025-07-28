using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EventManagement.Models;

public partial class EventContext : DbContext
{
    private static EventContext? dbContext;
    public static EventContext DbContext
    {
        get
        {
            if (dbContext == null)
                dbContext = new EventContext();
            return dbContext;
        }
    }
    public EventContext()
    {
    }

    public EventContext(DbContextOptions<EventContext> options)
        : base(options)
    {
    }

    public DbSet<Event> Events { get; set; } = null!;   

    public DbSet<User> Users { get; set; } = null!;

    public DbSet<Role> Roles { get; set; } = null!;   
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(@"Host=localhost;Port=5432;Database=Event;Username=postgres;Password=123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasOne(u => u.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RoleId);

        modelBuilder.Entity<Event>()
           .HasOne(e => e.User)
           .WithMany(u => u.Events)
           .HasForeignKey(e => e.UserId);     


    }   
}
