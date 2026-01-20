using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TurfSyncTurfSchedulingSportsSystem.Forms.AdminDashboardForms
{
    public class turflocation
    {
        public int Id {  get; set; }
        public string turf {  get; set; }
        public string Location { get; set; }
        public turflocation(string turf , string Location) {
            this.turf = turf;
            this.Location = Location;
            Id++;
            
        }
    }
}
