using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using RtdData;
using RtdData.Entities;

namespace RtdPlanner
{
    public class Schedule : Base
    {
      /*private List<Schedule> _stops = null;

      public override async Task InitAsync(string stopTimesFile)
      {
        await base.InitAsync(stopTimesFile);
        _initTask = Task.Run(() => {
          using (var reader = new StreamReader(stopTimesFile))
          using (var csv = new CsvReader(reader))
          {
            this._stops = csv.GetRecords<Schedule>().ToList();
          }
        });
        await _initTask;
        return;
      }

      // public void GetStopTimes(int stopId, TimeSpan start, TimeSpan end)
      // {
      //   base.WaitForLoading();
      //   var validStops = _stops.Where(stop => stop.StopId == stopId
      //                   && stop.DepartureTime >= start 
      //                   && stop.DepartureTime <= end)
      //                   .Select(s => s);
        
      //   validStops.ToList()
      //     .ForEach(s => Console.WriteLine($"Trip: {s.TripId} Depart: {s.DepartureTime}"));
      // }
*/
      ///<summary>
      /// Already looked of trips for a route, pass in here to get the stop
      /// times
      ///</summary>
      public List<Schedule> GetStopTimes(List<TripEntity> trips, TimeSpan start, TimeSpan end, int stopId)
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
  }

}