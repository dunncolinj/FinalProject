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
        private readonly Guid _userID;

        public LostPetService(Guid userID)
        {
            _userID = userID;
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
                        //.Where(e => e.UserId == _userID)
                        .Select(
                        e =>
                        new LostPetListItem
                        {
                            PetID = e.PetID,
                            Comments = e.Comments,
                            WhenLost = e.WhenLost
                        }
                  );
                return query.ToArray();
            }
        }

        public LostPetDetail GetLostPetByID(int petID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .LostPets
                    .Single(e => e.PetID == petID);
                return
                    new LostPetDetail
                    {
                       
                        PetID = entity.PetID,
                        Comments = entity.Comments,
                        WhenLost = entity.WhenLost
                    };
            }
        }

        public LostPetDetail GetPetOwner(int petID)
        {
            using (var ctx = new ApplicationDbContext())
            {

                var entity =
                    ctx
                    .LostPets
                    .Single(e => e.PetID == petID);
                return
                    new LostPetDetail
                    {
                        PetID = entity.PetID,
                        Comments = entity.Pet.User.Name
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
                        .Single(e => e.PetID == model.PetID);
                entity.PetID = model.PetID;
                entity.Comments = model.Comments;
                entity.WhenLost = model.WhenLost;


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
                    .Single(e => e.PetID == petID);

                ctx.LostPets.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
