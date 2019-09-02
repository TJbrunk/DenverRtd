using System;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using CsvHelper.TypeConversion;

namespace RtdData
{
    public class Schedule
    {
      
      [Name("trip_id")]
      public int TripId {get; set;}

      [Name("arrival_time"), TypeConverter(typeof(CustomTimeSpanConverter))]
      public TimeSpan ArrivalTime { get; set; }
      
      [Name("departure_time"), TypeConverter(typeof(CustomTimeSpanConverter))]
      public TimeSpan DepartureTime { get; set; }
      
      [Name("stop_id")]
      public int? StopId { get; set; }

      [Name("stop_sequence")]
      public int? Sequence { get; set; }
      
      [Name("stop_headsign")]
      public int? Headsign { get; set; }
      
      [Name("pickup_type")]
      public int? PickupType { get; set; }
      
      [Name("drop_off_type")]
      public int? DropoffType { get; set; }
      
      [Name("shape_dist_traveled")]
      public int? Distance { get; set; }
      
      [Name("timepoint")]
      public int? TimePoint { get; set; }

      public static void GetStopTimes(int stopId, TimeSpan start, TimeSpan end)
      {
        using (var reader = new StreamReader("../../google_transit/stop_times-short.txt"))
        using (var csv = new CsvReader(reader))
        {
          // csv.Configuration.RegisterClassMap<ScheduleMap>();
          var stops = csv.GetRecords<Schedule>();
          var validStops = stops.Where(stop => stop.StopId == stopId
                          && stop.DepartureTime >= start 
                          && stop.DepartureTime <= end)
                          .Select(s => s);
          
          validStops.ToList()
            .ForEach(s => Console.WriteLine($"Trip: {s.TripId} Depart: {s.DepartureTime}"));
        }
      }
    }

    // public class ScheduleMap : ClassMap<Schedule>
    // {
    //     public ScheduleMap()
    //     {
    //         Map(m => m.TripId).Name("trip_id");
    //         Map(m => m.DepartureTime).Name("departure_time").TypeConverter<TestConverter>();
    //     }
    // }

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