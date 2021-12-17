using FinalProject.Data;
using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Services
{
    public class PetService
    {
        public readonly Guid _userID;

        public PetService(Guid userID)
        {
            _userID = userID;
        }

        public bool CreatePet(PetCreate pet)
        {
            var entity =
                new Pet()
                {
                    Name = pet.Name,
                    Species = pet.Species,
                    Breed = pet.Breed,
                    Weight = pet.Weight,
                    MicrochipNumber = pet.MicrochipNumber,
                    UserID = _userID,
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Pets.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<PetListItem> GetPets()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Pets
                        .Where(e => e.UserID == _userID)
                        .Select(
                            e =>
                                new PetListItem
                                {
                                    Name = e.Name,
                                    Species = e.Species,
                                    Breed = e.Breed,
                                    Weight = e.Weight,
                                    MicrochipNumber = e.MicrochipNumber,
                                    UserID = _userID
                                }
                        );
                return query.ToArray();
            }
        }

        public PetDetail GetPetByID(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Pets
                        .Single(e => e.ID == id && e.UserID == _userID);
                return
                    new PetDetail
                    {
                        ID = entity.ID,
                        Name = entity.Name, 
                        Breed = entity.Breed, 
                        Weight = entity.Weight, 
                        Species = entity.Species, 
                        MicrochipNumber = entity.MicrochipNumber,
                        UserID = _userID
                    };
            }
        }

        public bool UpdatePet(PetEdit pet)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Pets
                        .Single(e => e.ID == pet.ID && e.UserID == _userID);
                entity.Name = pet.Name;
                entity.Species = pet.Species;
                entity.Breed = pet.Breed;
                entity.Weight = pet.Weight;
                entity.MicrochipNumber = pet.MicrochipNumber;
                entity.UserID = _userID;
                entity.User = pet.User;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeletePet(int ID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Pets
                        .Single(e => e.ID == ID && e.UserID == _userID);

                ctx.Pets.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
