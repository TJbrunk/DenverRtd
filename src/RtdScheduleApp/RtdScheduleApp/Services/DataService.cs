using Microsoft.EntityFrameworkCore;
using RtdData;
using RtdData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rtd.UI.Services
{
    public class TripStopDetails
    {
        public int TripId { get; set; }
        public string RouteId { get; set; }
        public int StopId { get; set; }
        public TimeSpan ArrivalTime { get; set; }
        public TimeSpan DepartureTime { get; set; }

    }
    public class DataService
    {
        ///<summary>
        /// Already looked of trips for a route, pass in here to get the stop
        /// times
        ///</summary>
        public List<TripStopDetails> GetStopTimes(List<TripEntity> trips, TimeSpan start, TimeSpan end, int stopId)
        {
            using (var db = new RtdDbContext())
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
                  select new TripStopDetails
                  {
                      TripId = trip.Id,
                      RouteId = trip.RouteId,
                      StopId = (int)stop.StopId,
                      ArrivalTime = stop.ArrivalTime,
                      DepartureTime = stop.DepartureTime
                  };
                //details.OrderBy(s => s.DepartureTime).ToList()
                //  .ForEach(s => Console.WriteLine($"{s.Id} {s.RouteId} {s.DepartureTime} {s.StopId}"));
                return details.OrderBy(s => s.ArrivalTime).ToList();
            }
        }

        /// <summary>
        /// Get Trips for the provided route(s) (FF1, 80L, etc)
        /// </summary>
        /// <param name="routeIds"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public List<TripEntity> GetTripsByRoute(List<string> routeIds, int? direction = null)
        {
            using (var db = new RtdDbContext())
            {
                var matchingTrips =
                  db.Trips.Where(trip =>
                              routeIds.Contains(trip.RouteId))
                        .OrderBy(t => t.Id)
                        .Select(s => s);

                if (direction != null)
                {
                    matchingTrips = matchingTrips.Where(t => t.DirectionId == direction);
                }

                //matchingTrips
                //  .ToList()
                //  .ForEach(t =>
                //    Console.WriteLine($"TripId: {t.Id} ServiceId: {t.ServiceId} Route: {t.RouteId} Dir: {t.DirectionId}")
                //  );

                return matchingTrips.ToList();
            }
        }
    }
}
