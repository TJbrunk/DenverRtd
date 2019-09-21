using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using CsvHelper.TypeConversion;

namespace RtdData.Entities
{
  [Table("StopTimes")]
  public class StopTimeEntity
  {
    [Name("trip_id")] // Unique ID for a trip (Route)
    public int TripId {get; set;}

    [Name("arrival_time"), TypeConverter(typeof(CustomTimeSpanConverter))]
    public TimeSpan ArrivalTime { get; set; }
    
    [Name("departure_time"), TypeConverter(typeof(CustomTimeSpanConverter))]
    public TimeSpan DepartureTime { get; set; }
    
    [Name("stop_id")]
    public int? StopId { get; set; }

    [Name("stop_headsign")]
    public int? Headsign { get; set; }

    public static async Task SetData(RtdDbContext db, string file)
    {
      using (var reader = new StreamReader(file))
      using (var csv = new CsvReader(reader))
      {
        var stoptimes = csv.GetRecords<StopTimeEntity>().ToList();
        await db.AddRangeAsync(stoptimes);
        await db.SaveChangesAsync();
      }
    }
  }

  public class CustomTimeSpanConverter : TimeSpanConverter
  {
    override public Object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
    {
        var t = text.Split(new char[1]{':'}, 3, StringSplitOptions.None);
        int hours = int.Parse(t[0]);
        int mins = int.Parse(t[1]);
        return new TimeSpan(hours, mins, 0);
    }
  }
}