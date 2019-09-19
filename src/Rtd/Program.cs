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
            Schedule s = new Schedule();

            List<string> routes = new List<string>(new string[] {"FF1", "FF3"});
            Trip trip = new Trip();
            
            var trips = trip.GetTripsByRoute(routes);
            
            TimeSpan start = new TimeSpan(7, 0, 0);
            TimeSpan end = new TimeSpan(9, 0, 0);
            s.GetStopTimes(trips, start, end, 34660);
        }

        private static async Task InitDb()
        {
            using(var db = new RtdDbContext())
            {
                await TripEntity.SetData(db, @"..\google_transit\trips.txt");
                await StopTimeEntity.SetData(db, @"..\google_transit\stop_times.txt");
            }
        }
    }
}
