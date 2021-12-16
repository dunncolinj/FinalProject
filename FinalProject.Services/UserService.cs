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
        private readonly int _userId;

        public UserService(int userId)
        {
            _userId = userId;
        }

        public bool CreateUser(UserCreate model)
        {
            var entity = new User()
            {
                Id = _userId,
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
                var query = ctx.Users.Where(e => e.Id == _userId).Select(e => new UserListItem
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
        }

        public bool UpdateUser(UserUpdate model)
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

        public bool DeleteUser(int Id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Users.Single(e => e.Id == Id);
                ctx.Users.Remove(entity);
                return (ctx.SaveChanges() == 1);
            }
        }
    }
}
