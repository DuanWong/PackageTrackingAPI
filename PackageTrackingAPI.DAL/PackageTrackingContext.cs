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

            modelBuilder.Entity<User>().HasData(
                new User { UserID = 1, Name = "Alice Johnson", Email = "alice.johnson@example.com", Role = "Admin", PasswordHash = "hashed_password_1" },
                new User { UserID = 2, Name = "Bob Smith", Email = "bob.smith@example.com", Role = "User", PasswordHash = "hashed_password_2" },
                new User { UserID = 3, Name = "Charlie Brown", Email = "charlie.brown@example.com", Role = "User", PasswordHash = "hashed_password_3" },
                new User { UserID = 4, Name = "David Wilson", Email = "david.wilson@example.com", Role = "Manager", PasswordHash = "hashed_password_4" },
                new User { UserID = 5, Name = "Emma Davis", Email = "emma.davis@example.com", Role = "User", PasswordHash = "hashed_password_5" }
            );

            modelBuilder.Entity<Alert>().HasData(
                new Alert { AlertID = 1, UserID = 1, PackageID = 101, Message = "Package delayed due to weather conditions.", Timestamp = DateTime.Parse("2025-04-11T10:30:00Z") },
                new Alert { AlertID = 2, UserID = 2, PackageID = 102, Message = "Package out for delivery.", Timestamp = DateTime.Parse("2025-04-11T12:45:00Z") },
                new Alert { AlertID = 3, UserID = 3, PackageID = 103, Message = "Package delivered successfully.", Timestamp = DateTime.Parse("2025-04-10T16:20:00Z") },
                new Alert { AlertID = 4, UserID = 4, PackageID = 104, Message = "Package returned to sender.", Timestamp = DateTime.Parse("2025-04-09T14:05:00Z") },
                new Alert { AlertID = 5, UserID = 5, PackageID = 105, Message = "Package pending at pickup location.", Timestamp = DateTime.Parse("2025-04-08T18:30:00Z") }
            );
        }
    }
}
