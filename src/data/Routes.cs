using CsvHelper.Configuration.Attributes;

namespace RtdData
{
    public class Route
    {
      [Index(0)]
      public string Id { get; set; } //FF7, 103W, etc
      [Index(1)]
      public string AgencyId { get; set; } // Always RTD
      [Index(2)]
      public string Name { get; set; }
      [Index(3)]
      public string LongName { get; set; }
      [Index(4)]
      public string Description { get; set; }
      [Index(5)]
      public int Type { get; set; }
      [Index(6)]
      public string Url { get; set; }
      [Index(7)]
      public string Color { get; set; }
      [Index(8)]
      public string TextColor { get; set; }

    }
}