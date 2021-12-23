using FinalProject.Data;
using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Services
{
    public class UserService
    {
        private readonly Guid _userId;

        public UserService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateUser(UserCreate model)
        {
            if (model is null) return false;

            var entity = new User()
            {
                Name = model.Name,
                Type = model.Type,
                Phone = model.Phone,
                Address = model.Address,
                City = model.City,
                State = model.State,
                Zip = model.Zip
            };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Users.Add(entity);
                return (ctx.SaveChanges() == 1);
            }
        }

        public IEnumerable<UserListItem> GetUsers()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Users.Select(e => new UserListItem
                {
                    Id = e.Id,
                    Name = e.Name,
                    Type = e.Type,
                    Phone = e.Phone,
                    Address = e.Address,
                    City = e.City,
                    State = e.State,
                    Zip = e.Zip
                }
                );

                return query.ToArray();
            }
        }

        public UserDetail GetUserByID(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                try
                {
                    var entity = ctx.Users.Single(e => e.Id == id);
                    return new UserDetail
                    {
                        Id = entity.Id,
                        Name = entity.Name,
                        Type = entity.Type,
                        Phone = entity.Phone,
                        Address = entity.Address,
                        City = entity.City,
                        State = entity.State,
                        Zip = entity.Zip
                    };
                }
                catch
                {
                    return null;
                }
            }
        }

        public UserDetail GetUserByName(string name)
        {
            try
            {
                using (var ctx = new ApplicationDbContext())
                {
                    var entity = ctx.Users.Single(e => e.Name == name);
                    return new UserDetail
                    {
                        Id = entity.Id,
                        Name = entity.Name,
                        Type = entity.Type,
                        Phone = entity.Phone,
                        Address = entity.Address,
                        City = entity.City,
                        State = entity.State,
                        Zip = entity.Zip
                    };
                }
            }
            catch
            {
                return null;
            }
        }

        public bool UpdateUser(UserUpdate model)
        {
            try
            {
                using (var ctx = new ApplicationDbContext())
                {
                    var entity = ctx.Users.Single(e => e.Id == model.Id);
                    entity.Name = model.Name;
                    entity.Type = model.Type;
                    entity.Phone = model.Phone;
                    entity.Address = model.Address;
                    entity.City = model.City;
                    entity.State = model.State;
                    entity.Zip = model.Zip;

                    return (ctx.SaveChanges() == 1);
                }
            }
            catch
            {
                return false;
            }

        }

        public bool DeleteUser(int Id)
        {
            // check if owner has pets; if so, do not allow deletion

            try
            {
                using (var ctx = new ApplicationDbContext())
                {
                    var query = ctx.Pets.Where(e => e.UserID == Id).Select(e => new PetListItem
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

                    if (query.ToArray().Length > 0) return false;
                }
            }
            catch
            {
                // no action required - code may proceed
            }
            try
            {
                using (var ctx = new ApplicationDbContext())
                {
                    var entity = ctx.Users.Single(e => e.Id == Id);

                    ctx.Users.Remove(entity);
                    return (ctx.SaveChanges() == 1);
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
