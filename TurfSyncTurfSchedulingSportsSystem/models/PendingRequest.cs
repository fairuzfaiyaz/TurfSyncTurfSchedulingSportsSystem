using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurfSyncTurfSchedulingSportsSystem.Models
{
    public class PendingRequest
    {
        public int RequestId { get; set; }
        public int TurfId { get; set; }
        public DateTime ScheduleDate { get; set; }
        public TimeSpan ScheduleTime { get; set; }
        public int Duration { get; set; }
        public decimal Price { get; set; }
        public string RequestedBy { get; set; } // username or fullname
        public string RequestStatus { get; set; } // Pending, Booked, Arrived
        public DateTime RequestedAt { get; set; }
    }

}
