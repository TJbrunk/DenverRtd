using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using RtdData;
using RtdData.Entities;

namespace RtdPlanner
{
    class Program
    {
        static void Main(string[] args)
        {
            // InitDb().Wait();
            // GetDetailsForRoutes();
            Schedule.GetNextTrips(34660, 50);//, new List<string>(new string[]{"FF6", "FF1"}));
        }

        private static async Task InitDb()
        {
            using(var db = new RtdDbContext())
            {
                await TripEntity.SetData(db, @"..\google_transit\trips.txt");
                await StopTimeEntity.SetData(db, @"..\google_transit\stop_times.txt");
            }
        }

        private static void GetDetailsForRoutes()
        {
            List<string> routes = new List<string>(new string[] {"FF1", "FF3"});
            
            var trips = Trip.GetTripsByRoute(routes);
            
            TimeSpan start = new TimeSpan(7, 0, 0);
            TimeSpan end = new TimeSpan(9, 0, 0);
            Schedule.GetStopTimes(trips, start, end, 34660);
        }
    }
}
