using Microsoft.EntityFrameworkCore;
using bike_webapi.Models;

namespace bike_webapi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Journey> Journeys { get; set; } = null!;
        public DbSet<Station> Stations { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var stationModel = modelBuilder.Entity<Station>();

            stationModel.Property(s => s.Id).ValueGeneratedNever();

            var journeyModel = modelBuilder.Entity<Journey>();
            
            journeyModel
                .HasOne(j => j.DepartureStation)
                .WithMany(ds => ds.Departures)
                .HasForeignKey(j => j.DepartureStationId);
            
            journeyModel
                .HasOne(j => j.ReturnStation)
                .WithMany(rs => rs.Returns)
                .HasForeignKey(j => j.ReturnStationId);
        }
    }
}
