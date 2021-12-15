using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class PetEdit
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Weight { get; set; }
        public int OwnerID { get; set; }
    }
}
