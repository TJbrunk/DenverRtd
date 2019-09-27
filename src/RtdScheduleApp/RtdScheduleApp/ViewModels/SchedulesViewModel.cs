using Rtd.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace RtdScheduleApp.ViewModels
{
    public class SchedulesViewModel : BaseViewModel
    {
        public List<Route> Routes { get; set; } = 
            new List<Route>
                (
                    new Route[]
                    {
                        new Route{Name = "FF1", IsSelected = true },
                        new Route{Name = "FF3", IsSelected = true },
                        new Route{Name = "FF7", IsSelected = true }
                    }
                );

        public List<TripStopDetails> StopDetails { get; set; } = new List<TripStopDetails>();

        public SchedulesViewModel()
        {
            Title = "Schedules";
            this.GetStopDetails();
        }

        public void GetStopDetails()
        {
            var selectedRoutes =
                this.Routes.Where(r => r.IsSelected)
                    .Select(r => r.Name)
                    .ToList();
            var db = DependencyService.Get<DataService>();
            var trips = db.GetTripsByRoute(selectedRoutes);
            TimeSpan start = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, 0);
            TimeSpan end = start.Add(new TimeSpan(0, 15, 0));
            int stopId = 34312; //ff1
            //int stopId = 34315; //FF3
            //22903; FF7
            if (start.Hours > 6 && start.Hours < 10)
            {
                stopId = 34660;
            }
            this.StopDetails = 
                db.GetStopTimes(trips.ToList(), start, end, stopId);
        }
    }

    public class Route
    {
        public string Name { get; set; }
        public bool IsSelected { get; set; }
    }
}
