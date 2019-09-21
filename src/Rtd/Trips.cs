using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using RtdData;
using RtdData.Entities;

namespace RtdPlanner
{
    public class Trip
    {

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