using System;

using RtdScheduleApp.Models;

namespace RtdScheduleApp.ViewModels
{
    public class ReminderDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }
        public ReminderDetailViewModel(Item item = null)
        {
            Title = item?.Text;
            Item = item;
        }
    }
}
