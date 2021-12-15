using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class PetListItem
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int UserID { get; set; }
        public Enum Species { get; set; }
    }
}
