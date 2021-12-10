using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Services
{
    public class LostPetService
    {
        private readonly Guid _ownerId;
        // private readonly Guid _postId;

        public LostPetService(Guid ownerId)
        {
            _ownerId = ownerId;
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
                        .Where(e => e.OwnerID == _ownerID)
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
                    .Replies
                    .Single(e => e.ID == ID && e.PetID == petID);
                return
                    new LostPetDetail
                    {
                        //CommentID = entity.CommentID
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
                    .Single(e => e.PetID == OwnerId && e.OwnerId == _ownerId);
                return
                    new LostPetDetail
                    {
                        PetId = entity.PetId
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
                        .Single(e => e.PetID == model.PetID && e.OwnerId == _ownerId);


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
                    .Single(e => e.PetId == petID && e.OwnerId == _ownerId);

                ctx.LostPets.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

    }
}
