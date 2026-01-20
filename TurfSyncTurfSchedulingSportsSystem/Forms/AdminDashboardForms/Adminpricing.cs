using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TurfSyncTurfSchedulingSportsSystem.Forms.AdminDashboardForms
{
    public class Adminpricing
    {
        public string packageName {  get; set; }
        public int price { get; set; }
        public int duration { get; set; }
        public bool available { get; set; }

        public Adminpricing(string packageName ,  int price , int duration , bool available) {
            this.packageName = packageName;
            this.price = price;
            this.duration = duration;
            this.available = available;
        }
    }
}
