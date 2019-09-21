using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using RtdData;
using RtdData.Entities;

namespace RtdPlanner
{
    public class Schedule
    {
      ///<summary>
      /// Already looked of trips for a route, pass in here to get the stop
      /// times
      ///</summary>
      public static List<Schedule> GetStopTimes(List<TripEntity> trips, TimeSpan start, TimeSpan end, int stopId)
      {
        using(var db = new RtdDbContext())
        {
          var stops =
            db.StopTimes.Where(stop =>
                        trips.Any(t => t.Id == stop.TripId)
                        && stop.DepartureTime >= start 
                        && stop.DepartureTime <= end
                        && stop.StopId == stopId)
                  .Select(s => s);

          var details = 
            from trip in trips
            join stop in stops on trip.Id equals stop.TripId
            select new {
              trip.Id, trip.RouteId, trip.DirectionId, stop.StopId, trip.ServiceId, stop.DepartureTime
            };
          details.OrderBy(s => s.DepartureTime).ToList()
            .ForEach(s => Console.WriteLine($"{s.Id} {s.RouteId} {s.DepartureTime} {s.StopId}"));

          return null;
        }
      }

      // Gets the next trips that for the given Stop within the timeSpan (minutes)
      // StopId : 34660, etc
      public static void GetNextTrips(int stopId, int timeSpan = 20, List<string> routes = null)
      {
        TimeSpan start = new TimeSpan(17, 0, 0);//DateTime.Now.TimeOfDay;
        TimeSpan end = start.Add(new TimeSpan(0, timeSpan, 0));
        using(var db = new RtdDbContext())
        {
          var stops =
            db.StopTimes.Where(stop =>
                          stop.StopId == stopId
                          && stop.DepartureTime >= start
                          && stop.DepartureTime <= end);

          var trips =
            from trip in db.Trips
            join stop in stops on trip.Id equals stop.TripId
            select new {
              trip.Id, trip.RouteId, trip.ServiceId, stop.DepartureTime
            };
          
          if(routes != null)
          {
            trips = trips.Where(t => routes.Contains(t.RouteId));
          }
          
          trips
            .OrderBy(t => t.DepartureTime)
            .ToList()
            .ForEach(s => Console.WriteLine($"{s.Id} {s.RouteId} {s.DepartureTime}"));
        }
      }
  }

}