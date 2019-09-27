using RtdScheduleApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RtdScheduleApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SchedulesPage : ContentPage
	{
        private readonly SchedulesViewModel viewModel;
		public SchedulesPage()
		{
			InitializeComponent();
            BindingContext = viewModel = new SchedulesViewModel();
            //viewModel.GetStopDetails();

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

        }
    }
}