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
            if (pet is null) return false;

            var entity =
                new Pet()
                {
                    Name = pet.Name,
                    Species = pet.Species,
                    Breed = pet.Breed,
                    Weight = pet.Weight,
                    MicrochipNumber = pet.MicrochipNumber,
                    UserID = pet.UserID
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
                        .Select(
                            e =>
                                new PetListItem
                                {
                                    ID = e.ID,
                                    Name = e.Name,
                                    Species = e.Species,
                                    Breed = e.Breed,
                                    Weight = e.Weight,
                                    MicrochipNumber = e.MicrochipNumber,
                                    UserID = e.UserID
                                }
                        );
                return query.ToArray();
            }
        }

        public IEnumerable<PetListItem> GetPetsByUserID(int userID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Pets
                        .Where(e => e.UserID == userID)
                        .Select(
                            e =>
                    new PetListItem
                    {
                        ID = e.ID,
                        Name = e.Name,
                        Breed = e.Breed,
                        Weight = e.Weight,
                        Species = e.Species,
                        MicrochipNumber = e.MicrochipNumber,
                        UserID = e.UserID
                    }
                    );
                return query.ToArray();
            }
        }

        public PetDetail GetPetByChipID(string chipID)
        {
            try
            {
                using (var ctx = new ApplicationDbContext())
                {

                    var entity =
                    ctx
                        .Pets
                        .Single(e => e.MicrochipNumber == chipID);
                    return
                        new PetDetail
                        {
                            ID = entity.ID,
                            Name = entity.Name,
                            Breed = entity.Breed,
                            Weight = entity.Weight,
                            Species = entity.Species,
                            MicrochipNumber = entity.MicrochipNumber,
                            UserID = entity.UserID
                        };
                }
            }

            catch
            {
                return null;
            }
        }

        public PetDetail GetPetOwner(int userID)
        {
            try
            {
                using (var ctx = new ApplicationDbContext())
                {

                    var entity =
                        ctx
                            .Pets
                            .Single(e => e.User.Id == userID);
                    var entity2 =
                        ctx
                            .Users
                            .Single(e => e.Id == entity.UserID);
                    return
                        new PetDetail
                        {
                            ID = entity.ID,
                            Name = entity.Name,
                            Breed = entity.Breed,
                            Weight = entity.Weight,
                            Species = entity.Species,
                            MicrochipNumber = entity.MicrochipNumber,
                            UserID = entity.UserID
                        };
                }
            }
            catch
            {
                return null;
            }
        }

        public bool UpdatePet(PetEdit pet)
        {
            try
            {
                using (var ctx = new ApplicationDbContext())
                {
                    var entity =
                    ctx
                         .Pets
                         .Single(e => e.ID == pet.ID);
                    entity.Name = pet.Name;
                    entity.Species = pet.Species;
                    entity.Breed = pet.Breed;
                    entity.Weight = pet.Weight;
                    entity.MicrochipNumber = pet.MicrochipNumber;
                    entity.UserID = pet.UserID;
                    entity.User = pet.User;

                    return ctx.SaveChanges() == 1;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool DeletePet(int ID)
        {
            try
            {
                using (var ctx = new ApplicationDbContext())
                {
                    var entity =
                        ctx
                            .Pets
                            .Single(e => e.ID == ID);

                    ctx.Pets.Remove(entity);

                    return ctx.SaveChanges() == 1;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}

