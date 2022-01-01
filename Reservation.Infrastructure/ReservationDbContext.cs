using System;
using Microsoft.EntityFrameworkCore;
using Reservation.Domain.Enums;
using Reservation.Domain.Models;
using Reservation.Infrastructure.Implementations;

namespace Reservation.Infrastructure
{
    public class ReservationDbContext : DbContextBase
    {
        public DbSet<HourCapacity> HourCapacities { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        
        public ReservationDbContext(DbContextOptions options) :base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HourCapacity>().Property(e => e.WeekDay)
                .HasConversion(
                    v => v.ToString(),
                    v => (WeekDay)Enum.Parse(typeof(WeekDay), v));  
            
        }
    }
}