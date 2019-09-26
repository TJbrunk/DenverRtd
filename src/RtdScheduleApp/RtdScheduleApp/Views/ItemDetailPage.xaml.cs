using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using RtdScheduleApp.Models;
using RtdScheduleApp.ViewModels;

namespace RtdScheduleApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailPage : ContentPage
    {
        ReminderDetailViewModel viewModel;

        public ItemDetailPage(ReminderDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public ItemDetailPage()
        {
            InitializeComponent();

            var item = new Item
            {
                Text = "Item 1",
                Description = "This is an item description."
            };

            viewModel = new ReminderDetailViewModel(item);
            BindingContext = viewModel;
        }
    }
}