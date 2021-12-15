using FinalProject.Models;
using FinalProject.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FinalProject.WebAPI.Controllers
{
    [Authorize]
    public class UserController : ApiController
    {
        private UserService CreateUserService()
        {
            var Id = int.Parse(User.Identity.GetUserId());
            var userService = new UserService(Id);
            return userService;
        }

        public IHttpActionResult Get()
        {
            UserService userService = CreateUserService();
            var users = userService.GetUsers();
            return Ok(users);
        }

        public IHttpActionResult Post(UserCreate user)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var service = CreateUserService();
            if (!service.CreateUser(user)) return InternalServerError();
            return Ok();
        }

        public IHttpActionResult Get(int id)
        {
            UserService userService = CreateUserService();
            var user = userService.GetUserByID(id);
            return Ok(user);
        }

    }
}
