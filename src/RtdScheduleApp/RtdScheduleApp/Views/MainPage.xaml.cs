using Rtd.UI.Services;
using RtdScheduleApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RtdScheduleApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MasterDetailPage
    {
        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
        public MainPage()
        {
            InitializeComponent();

            MasterBehavior = MasterBehavior.Popover;

            //MenuPages.Add((int)MenuItemType.Reminders, (NavigationPage)Detail);
            //MenuPages.Add((int)MenuItemType.Schedules, (NavigationPage)Detail);
        }

        public async Task NavigateFromMenu(int id)
        {
            // Check if the menu page has been opened before.
            // If NOT, Initialize it and add it to the page list.
            if (!MenuPages.ContainsKey(id))
            {
                switch (id)
                {
                    case (int)MenuItemType.Reminders:
                        MenuPages.Add(id, new NavigationPage(new RemindersPage()));
                        break;
                    case (int)MenuItemType.About:
                        MenuPages.Add(id, new NavigationPage(new AboutPage()));
                        break;
                    case (int)MenuItemType.Schedules:
                        MenuPages.Add(id, new NavigationPage(new SchedulesPage()));
                        break;
                }
            }

            var newPage = MenuPages[id];

            if (newPage != null && Detail != newPage)
            {
                Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;
            }
        }
    }
}