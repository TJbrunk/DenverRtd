using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;

namespace RtdData
{
    class Program
    {
        static void Main(string[] args)
        {
            // TextReader reader = new StreamReader("../../google_transit/routes.txt");
            // var csvReader = new CsvReader(reader);
            // var routes = csvReader.GetRecords<Route>();
            // foreach (var r in routes)
            // {
            //     if(r.Id == "FF7")
            //     {
            //         Console.WriteLine("Found FF7 route");
            //     }
            // }
            TimeSpan start = new TimeSpan(7, 0, 0);
            TimeSpan end = new TimeSpan(9, 0, 0);
            Schedule.GetStopTimes(34660, start, end);
        }
    }
}
