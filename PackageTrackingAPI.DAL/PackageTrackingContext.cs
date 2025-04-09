using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PackageTrackingAPI.Models;

namespace PackageTrackingAPI.DAL
{
    public class PackageTrackingContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<TrackingEvent> TrackingEvents { get; set; }
        public DbSet<Alert> Alerts { get; set; }

        public PackageTrackingContext(DbContextOptions<PackageTrackingContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TrackingEvent>()
                .HasKey(te => te.EventID);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Packages)
                .WithOne(p => p.Sender)
                .HasForeignKey(p => p.SenderID);

            modelBuilder.Entity<Package>()
                .HasMany(p => p.TrackingEvents)
                .WithOne(te => te.Package)
                .HasForeignKey(te => te.PackageID);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Alerts)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserID);
        }
    }
}
