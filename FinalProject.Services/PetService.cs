﻿using FinalProject.Data;
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
        public readonly int _userID;

        public PetService(int userID)
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
                    //MicrochipNumber = pet.MicrochipNumber,
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
                                    ID = e.ID,
                                    Name = e.Name,
                                    UserID = _userID
                                    //Species??
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
                        //name, breed, weight, species, microchip number
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
                //entity.ID = pet.ID;
                entity.Name = pet.Name;
                entity.Weight = pet.Weight;
                entity.UserID = _userID;

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