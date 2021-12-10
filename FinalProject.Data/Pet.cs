using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Data
{
    public class Pet
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public Enum Species { get; set; }
        public string Breed { get; set; }
        public int Weight { get; set; }
        [Required]
        public string MicrochipNumber { get; set; }
        [ForeignKey(nameof(UserID))]
        public int UserID { get; set; }
    }
}
