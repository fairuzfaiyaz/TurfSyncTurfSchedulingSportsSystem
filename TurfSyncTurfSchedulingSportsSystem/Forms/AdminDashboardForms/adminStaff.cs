using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurfSyncTurfSchedulingSportsSystem.Forms.AdminDashboardForms
{
    public class adminStaff
    {
        public string Name { get; set; }
        public string email { get; set; }
        public string password { get; set; }   
        public string role { get; set; }
        public adminStaff(string Name, string email, string password, string role)
        {
            this.Name = Name;
            this.email = email;
            this.password = password;
            this.role = role;
        }
    }
}
