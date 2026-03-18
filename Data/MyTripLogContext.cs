using Microsoft.EntityFrameworkCore;
using MyTripLog.Models;

namespace MyTripLog.Data
{
    public class MyTripLogContext : DbContext
    {
        public MyTripLogContext(DbContextOptions<MyTripLogContext> options)
            : base(options) { }

        public DbSet<Trip> Trips => Set<Trip>();
        public DbSet<Destination> Destinations => Set<Destination>();
        public DbSet<Accommodation> Accommodations => Set<Accommodation>();
        public DbSet<Activity> Activities => Set<Activity>();
        public DbSet<TripActivity> TripActivities => Set<TripActivity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Trip>()
                .HasOne(t => t.Destination)
                .WithMany(d => d.Trips)
                .HasForeignKey(t => t.DestinationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Trip>()
                .HasOne(t => t.Accommodation)
                .WithMany(a => a.Trips)
                .HasForeignKey(t => t.AccommodationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TripActivity>()
                .HasKey(ta => new { ta.TripId, ta.ActivityId });
        }
    }
}