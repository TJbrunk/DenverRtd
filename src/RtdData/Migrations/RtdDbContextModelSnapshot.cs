﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RtdData;

namespace RtdData.Migrations
{
    [DbContext(typeof(RtdDbContext))]
    partial class RtdDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity("RtdData.Entities.StopTimeEntity", b =>
                {
                    b.Property<int?>("StopId");

                    b.Property<int>("TripId");

                    b.Property<TimeSpan>("ArrivalTime");

                    b.Property<TimeSpan>("DepartureTime");

                    b.Property<int?>("Headsign");

                    b.HasKey("StopId", "TripId");

                    b.HasIndex("DepartureTime");

                    b.HasIndex("StopId");

                    b.HasIndex("TripId");

                    b.ToTable("StopTimes");
                });

            modelBuilder.Entity("RtdData.Entities.TripEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DirectionId");

                    b.Property<string>("RouteId");

                    b.Property<string>("ServiceId");

                    b.HasKey("Id");

                    b.HasIndex("RouteId");

                    b.ToTable("Trips");
                });
#pragma warning restore 612, 618
        }
    }
}
