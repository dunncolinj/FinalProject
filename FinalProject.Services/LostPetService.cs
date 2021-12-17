using FinalProject.Data;
using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Services
{
    public class LostPetService
    {
        private readonly Guid _ownerID;

        public LostPetService(Guid ownerID)
        {
            _ownerID = ownerID;
        }

        public bool CreateLostPet(LostPetCreate model)
        {
            var entity =
                new LostPet()
                {
                    ID = model.ID,
                    Comments = model.Comments,
                    WhenLost = DateTime.Now,
                    PetID = model.PetID
                };


            using (var ctx = new ApplicationDbContext())
            {
                ctx.LostPets.Add(entity);
                return ctx.SaveChanges() == 1;
            }

        }
        public IEnumerable<LostPetListItem> GetLostPets()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .LostPets
                        .Where(e => e.ID == _ownerID)
                        .Select(
                        e =>
                        new LostPetListItem
                        {
                            PetID = e.PetID,
                            Comments = e.Comments,
                        }
                  );
                return query.ToArray();
            }
        }

        public LostPetDetail GetLostPetByID(int ID, int petID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .LostPets
                    .Single(e => e.ID == ID && e.PetID == petID);
                return
                    new LostPetDetail
                    {
                       
                        PetID = entity.PetID,
                        Comments = entity.Comments
                    };
            }
        }

        public LostPetDetail GetPetOwner(int ownerID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .LostPets
                    .Single(e => e.PetID == ownerID && e.ID == _ownerID);
                return
                    new LostPetDetail
                    {
                        PetID = entity.PetID
                    };
            }
        }

        public bool UpdateByID(LostPetEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .LostPets
                        .Single(e => e.PetID == model.PetID && e.ID == _ownerID);


                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteByID(int petID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .LostPets
                    .Single(e => e.PetID == petID && e.ID == _ownerID);

                ctx.LostPets.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
