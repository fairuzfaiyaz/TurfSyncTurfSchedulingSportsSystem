using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurfSyncTurfSchedulingSportsSystem.Forms.AdminDashboardForms
{
    public class AdminTurfBookings
    {
        public string Turf { get; set; }
        public string customerName { get; set; }    
        public string Date { get; set; }
        public string time { get; set; }

        public AdminTurfBookings(String Turf , String customerName, string date ,  string time)
        {
            this.Turf = Turf;
            this.customerName = customerName;
            this.Date= date;
            this.time = time;

        }
        
    }
}
