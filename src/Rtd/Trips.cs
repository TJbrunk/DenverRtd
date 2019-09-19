using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using RtdData;
using RtdData.Entities;

namespace RtdPlanner
{
    public class Trip : Base
    {
/*      private List<Trip> _trips = null;
      
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
*/
      // Get the Trip information for the provided routes
      // Routes are FF7, 80L, etc
      public static List<TripEntity> GetTripsByRoute(List<string> routeIds, int? direction = null)
      {

        using(var db = new RtdDbContext())
        {
          var matchingTrips = 
            db.Trips.Where(trip =>
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
}