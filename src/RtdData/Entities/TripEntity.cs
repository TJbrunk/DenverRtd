using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration.Attributes;

namespace RtdData.Entities
{
  [Table("Trips")]
  public class TripEntity
  {
    // Route Name (FF7, 80L, etc)
    [Name("route_id")]
    public string RouteId { get; set; }

    // WK = Weekday
    // SA = Saturday
    // SU = Sunday
    // Holiday / Special
    [Name("service_id")]
    public string ServiceId { get; set; }

    // Unique ID for the Route/Service
    [Name("trip_id")]
    public int Id { get; set; }

    // [Name("trip_headsign")] // Text displayed on the Bus
    // public string Headsign { get; set; }

    // 0: Travel in one direction of your choice, such as outbound travel.
    // 1: Travel in the opposite direction, such as inbound travel.
    [Name("direction_id")]
    public int DirectionId { get; set; }

    // [Name("block_id")]
    // public string BlockId { get; set; }

    public static async Task SetData(RtdDbContext db, string file)
    {
      using (var reader = new StreamReader(file))
      using (var csv = new CsvReader(reader))
      {
        var trips = csv.GetRecords<TripEntity>().ToList();
        await db.AddRangeAsync(trips);
        await db.SaveChangesAsync();
      }
    }
  }
}