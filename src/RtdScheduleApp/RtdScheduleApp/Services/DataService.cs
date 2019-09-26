using Microsoft.EntityFrameworkCore;
using RtdData;
using RtdData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rtd.UI.Services
{
    public class DataService
    {
        ///<summary>
        /// Already looked of trips for a route, pass in here to get the stop
        /// times
        ///</summary>
        public void GetStopTimes(List<TripEntity> trips, TimeSpan start, TimeSpan end, int stopId)
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
                  select new
                  {
                      trip.Id,
                      trip.RouteId,
                      trip.DirectionId,
                      stop.StopId,
                      trip.ServiceId,
                      stop.DepartureTime
                  };
                //details.OrderBy(s => s.DepartureTime).ToList()
                //  .ForEach(s => Console.WriteLine($"{s.Id} {s.RouteId} {s.DepartureTime} {s.StopId}"));
                //return stops;
            }
        }

        /// <summary>
        /// Get Trips for the provided route(s) (FF1, 80L, etc)
        /// </summary>
        /// <param name="routeIds"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public IQueryable<TripEntity> GetTripsByRoute(List<string> routeIds, int? direction = null)
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

                return matchingTrips;
            }
        }
    }
}
