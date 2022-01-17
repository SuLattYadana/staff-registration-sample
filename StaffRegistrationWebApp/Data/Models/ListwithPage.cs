using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StaffRegistrationWebApp.Data.Models
{
    public class ListwithPage<Stafftb>
    {
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public IPagedList<Stafftb> Results { get; set; }
    }
}