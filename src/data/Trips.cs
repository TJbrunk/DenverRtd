using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration.Attributes;

namespace RtdData
{
    public class Trip : Base
    {
      [Name("route_id")]
      public string RouteId { get; set; }

      [Name("service_id")]
      public string ServiceId { get; set; }

      [Name("trip_id")]
      public int Id { get; set; }

      // [Name("trip_headsign")]
      // public string Headsign { get; set; }

      // [Name("direction_id")]
      // public int DirectionId { get; set; }

      // [Name("block_id")]
      // public string BlockId { get; set; }

      private List<Trip> _trips = null;
      public override async Task InitAsync(string tripsFile)
      {
        _initTask = Task.Run(() => {
          using (var reader = new StreamReader(tripsFile))
          using (var csv = new CsvReader(reader))
          {
            _trips = csv.GetRecords<Trip>().ToList();
          }
        });
        await _initTask;
        return;
      }

      public List<Trip> GetTripsByRoute(string routeId)
      {
        base.WaitForLoading();
        var matchingTrips = 
          _trips.Where(trip =>
                      trip.RouteId == routeId)
                .Select(s => s)
                .ToList();
        
        // matchingTrips
        //   .ForEach(t =>
        //     Console.WriteLine($"TripId: {t.Id} ServiceId: {t.ServiceId} Route: {routeId}")
        //   );
        return matchingTrips;
      }

      public List<Trip> GetTripsByRoute(List<string> routeIds)
      {
        base.WaitForLoading();

        var matchingTrips = 
          _trips.Where(trip =>
                      routeIds.Contains(trip.RouteId) )
                .Select(s => s)
                .ToList();
        
        // matchingTrips
        //   .ForEach(t =>
        //     Console.WriteLine($"TripId: {t.Id} ServiceId: {t.ServiceId} Route: {t.RouteId}")
        //   );
        return matchingTrips;
      }
    }
}