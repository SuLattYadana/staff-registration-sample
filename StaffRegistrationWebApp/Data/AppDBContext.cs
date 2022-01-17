using StaffRegistrationWebApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace StaffRegistrationWebApp.Data
{
    public class AppDBContext: DbContext
    {
        public AppDBContext() : base("Name=DefaultConnection")
        {

        }
        public DbSet<Stafftb> Staffs { get; set; }
        public DbSet<Accounttb> Accounts { get; set; }
    }
}