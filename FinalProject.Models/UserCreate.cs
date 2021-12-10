using FinalProject.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    class UserCreate
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public UserType Type { get; set; }
        [Required]
        [MaxLength(12, ErrorMessage = "Maximum length of 12 characters including separators")]
        public string Phone { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Limit of 100 characters")]
        public string Address { get; set; }
        [Required]
        [MaxLength(25, ErrorMessage = "Limit of 25 characters")]
        public string City { get; set; }
        [Required]
        [MaxLength(2, ErrorMessage = "State must be two-letter code")]
        public string State { get; set; }
        [Required]
        [MaxLength(10, ErrorMessage = "ZIP+4 must be less than 10 characters including separator")]
        public string Zip { get; set; }
    }
}
