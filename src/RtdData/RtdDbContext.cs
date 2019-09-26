using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RtdData.Entities;
using static System.Environment;

namespace RtdData
{
    public class RtdDbContext : DbContext
    {
        private static string _DbPath;
        public static string DbPath
        {
            get {
                if(!string.IsNullOrEmpty(_DbPath))
                {
                    return _DbPath;
                }
                else
                {
                    string appData = Environment.GetFolderPath(SpecialFolder.LocalApplicationData);
                    string folder = Path.Combine(appData, "RtdApp");
                    Directory.CreateDirectory(folder);
                    string db = Path.Combine(folder, "RtdData.db");
                    _DbPath = db;
                    return db;
                }
            }
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlite($"Data Source={DbPath};");
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TripEntity>()
                .HasIndex(t => t.RouteId);


             modelBuilder.Entity<StopTimeEntity>()
                .HasKey(s => new { s.StopId, s.TripId });

            modelBuilder.Entity<StopTimeEntity>()
                .HasIndex(s => s.DepartureTime);

            modelBuilder.Entity<StopTimeEntity>()
                .HasIndex(s => s.StopId);

            modelBuilder.Entity<StopTimeEntity>()
                .HasIndex(s => s.TripId);
        }


        public DbSet<TripEntity> Trips { get; set; }
        public DbSet<StopTimeEntity> StopTimes { get; set; }
    }
}
