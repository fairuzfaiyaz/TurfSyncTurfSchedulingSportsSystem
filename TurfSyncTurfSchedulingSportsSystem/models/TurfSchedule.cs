using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurfSyncTurfSchedulingSportsSystem.Models
{
    public class TurfSchedule
    {
        public int TurfId { get; set; }
        public DateTime ScheduleDate { get; set; }
        public TimeSpan ScheduleTime { get; set; }
        public string TurfLocation { get; set; }
        public int Duration { get; set; } // in minutes
        public decimal Price { get; set; }
        public string Status { get; set; } // Available, Booked, Maintenance
    }
}
