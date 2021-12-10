using FinalProject.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class UserListItem
    {
        public int Id { get; set; }
        [Display(Name="Full Name")]
        public string Name { get; set; }
        [Display(Name="Account Type")]
        public UserType Type { get; set; }
        [Display(Name="Phone Number")]
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }
}
