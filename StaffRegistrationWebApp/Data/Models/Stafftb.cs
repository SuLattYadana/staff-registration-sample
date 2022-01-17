using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StaffRegistrationWebApp.Data.Models
{
    public class Stafftb
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Address { get; set; }
        public string State { get; set; }
        [Required]
        public string Township { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Role { get; set; }
        [Required]
        public string JobTile { get; set; }
        [Required]
        public string Department { get; set; }
        [Required]
        public DateTime DateOfJoin { get; set; }
        public Nullable<bool> IsAdmin { get; set; }
        [Required]
        public string Nationality { get; set; }
        [Required]
        public string Religion { get; set; }
        [Required]
        public string Qualification { get; set; }

        public Nullable<System.DateTime> CreatedDateTime { get; set; }
    }
}