using FinalProject.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class PetCreate
    {
        
        [MinLength(1, ErrorMessage = "You must enter in a name for the pet.")]
        [MaxLength(45, ErrorMessage = "The name you have entered is too long.")]
        public string Name { get; set; }

        
        [MinLength(1, ErrorMessage = "You must enter in a number.")]
        [MaxLength(6, ErrorMessage = "The number you have entered is too long.")]
        public string MicrochipNumber { get; set; }

        public int UserID { get; set; }

        public string Breed { get; set; }
        public int Weight { get; set; }
        public Species Species { get; set; }
    }
}
