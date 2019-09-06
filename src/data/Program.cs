using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;

namespace RtdData
{
    class Program
    {
        static void Main(string[] args)
        {
            Schedule s = new Schedule();
            s.InitAsync("../../google_transit/stop_times.txt").ConfigureAwait(false);

            List<string> routes = new List<string>(new string[] {"FF7", "FF3"});
            Trip trip = new Trip();
            trip.InitAsync("../../google_transit/trips.txt").ConfigureAwait(false);
            var trips = trip.GetTripsByRoute(routes);

            TimeSpan start = new TimeSpan(7, 0, 0);
            TimeSpan end = new TimeSpan(9, 0, 0);
            s.GetStopTimes(trips, start, end);

        }
    }
}
