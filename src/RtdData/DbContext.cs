﻿using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using RtdData.Entities;

namespace RtdData
{
    public class RtdDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var db = Path.Combine(Environment.CurrentDirectory, "db", "RtdData.db");
            // string db = @"C:\Projects\Rtd-Planner\source\db\RtdData.db";
            optionsBuilder
                .UseSqlite($"Data Source={db};");
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
