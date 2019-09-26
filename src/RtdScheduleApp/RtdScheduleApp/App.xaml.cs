using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RtdScheduleApp.Views;
using Rtd.UI.Services;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace RtdScheduleApp
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            DependencyService.Register<DataService>();
            var db = DependencyService.Get<DataService>();
            

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
