﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RtdScheduleApp.Models
{
    public enum MenuItemType
    {
        Reminders,
        Schedules,
        About
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
