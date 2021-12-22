using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Data
{
    public class LostPet
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey(nameof(Pet))]
        public int PetID { get; set; }
        public virtual Pet Pet { get; set; }

        public DateTime WhenLost { get; set; }

        public string Comments { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        public virtual User User { get; set; }

    }
}
