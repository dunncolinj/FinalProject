using FinalProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class LostPetDetail
    {
        public int PetID { get; set; }
        public string Comments { get; set; }
        public DateTime WhenLost { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
    }
}
