using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using CsvHelper.TypeConversion;

namespace RtdData
{
    public class Schedule : Base
    {
      [Name("trip_id")]
      public int TripId {get; set;}

      [Name("arrival_time"), TypeConverter(typeof(CustomTimeSpanConverter))]
      public TimeSpan ArrivalTime { get; set; }
      
      [Name("departure_time"), TypeConverter(typeof(CustomTimeSpanConverter))]
      public TimeSpan DepartureTime { get; set; }
      
      [Name("stop_id")]
      public int? StopId { get; set; }

      [Name("stop_headsign")]
      public int? Headsign { get; set; }


      private List<Schedule> _stops = null;

      public override async Task InitAsync(string stopTimesFile)
      {
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

      ///<summary>
      /// Already looked of trips for a route, pass in here to get the stop
      /// times
      ///</summary>
      public List<Schedule> GetStopTimes(List<Trip> trips, TimeSpan start, TimeSpan end)
      {
        base.WaitForLoading();
        var validStops =
          _stops.Where(stop =>
                      trips.Any(t => t.Id == stop.TripId)
                      && stop.DepartureTime >= start 
                      && stop.DepartureTime <= end)
                .Select(s => s)
                .ToList();
        
        validStops
          .ForEach(s => Console.WriteLine($"Trip: {s.TripId} Depart: {s.DepartureTime}"));

        return validStops;
      }
  }

    public class CustomTimeSpanConverter : TimeSpanConverter
    {
      override public Object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
      {
          var t = text.Split(":", 3, StringSplitOptions.None);
          int hours = int.Parse(t[0]);
          int mins = int.Parse(t[1]);
          return new TimeSpan(hours, mins, 0);
      }
    }
}