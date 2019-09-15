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
      // Route Name (FF7, 80L, etc)
      [Name("route_id")]
      public string RouteId { get; set; }

      // WK = Weekday
      // SA = Saturday
      // SU = Sunday
      // Holiday / Special
      [Name("service_id")]
      public string ServiceId { get; set; }

      // Uniquie ID for the Route/Service
      [Name("trip_id")]
      public int Id { get; set; }

      // [Name("trip_headsign")] // Text displayed on the Bus
      // public string Headsign { get; set; }

      // 0: Travel in one direction of your choice, such as outbound travel.
      // 1: Travel in the opposite direction, such as inbound travel.
      [Name("direction_id")]
      public int DirectionId { get; set; }

      // [Name("block_id")]
      // public string BlockId { get; set; }

      private List<Trip> _trips = null;
      
      /// Load items from the csv file in prep for future calls
      public override async Task InitAsync(string tripsFile)
      {
        await base.InitAsync(tripsFile);
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

      /// RouteIds are FF7, FF3, 80L, etc
      public List<Trip> GetTripsByRoute(string routeId, int? direction = null)
      {
        base.WaitForLoading();
        var matchingTrips = 
          _trips.Where(trip =>
                      trip.RouteId == routeId)
                .OrderBy(t => t.Id)
                .Select(s => s);
        
        if(direction != null)
        {
          matchingTrips = matchingTrips.Where(t => t.DirectionId == direction);
        }
        // matchingTrips.ToList()
        //   .ForEach(t =>
        //     Console.WriteLine($"TripId: {t.Id} ServiceId: {t.ServiceId} Route: {routeId}")
        //   );
        return matchingTrips.ToList();
      }

      // Get the Trip information for the provided routes
      // Routes are FF7, 80L, etc
      public List<Trip> GetTripsByRoute(List<string> routeIds, int? direction = null)
      {
        base.WaitForLoading();

        var matchingTrips = 
          _trips.Where(trip =>
                      routeIds.Contains(trip.RouteId) )
                .OrderBy(t => t.Id)
                .Select(s => s);

        if(direction != null)
        {
          matchingTrips = matchingTrips.Where(t => t.DirectionId == direction);
        }
        
        // matchingTrips
        //   .ToList()
        //   .ForEach(t =>
        //     Console.WriteLine($"TripId: {t.Id} ServiceId: {t.ServiceId} Route: {t.RouteId} Dir: {t.DirectionId}")
        //   );
        return matchingTrips.ToList();
      }
    }
}