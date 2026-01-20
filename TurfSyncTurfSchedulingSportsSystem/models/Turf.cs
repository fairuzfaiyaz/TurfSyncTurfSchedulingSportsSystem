using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurfSyncTurfSchedulingSportsSystem.Models
{
    internal class Turf
    {
        public int TurfId { get; set; }
        public string TurfLocation { get; set; }
        public DateTime ScheduleDate { get; set; }
        public TimeSpan ScheduleTime { get; set; }
        public int Duration { get; set; } // in minutes
        public decimal Price { get; set; }
        public string Status { get; set; } // Pending, Approved, Booked, Cancelled
        public DateTime CreatedAt { get; set; }
    }
}
