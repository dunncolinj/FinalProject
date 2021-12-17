using FinalProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class PetDetail
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Species Species { get; set; }
        public string Breed { get; set; }
        public int Weight { get; set; }
        public string MicrochipNumber { get; set; }
        public Guid UserID { get; set; }

    }
}
