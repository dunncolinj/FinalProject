using FinalProject.Models;
using FinalProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace FinalProject.WebAPI.Controllers
{
    [Authorize]
    public class LostPetController : ApiController
    {
        private LostPetService CreateLostPetService()
        {
            var ID = Guid.Parse(User.Identity.GetUserId());
            var lostPetService = new LostPetService(ID);
            return lostPetService;
        }

        public IHttpActionResult Post(LostPetCreate lostPet)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateLostPetService();

            if (!service.CreateLostPet(lostPet))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Get()
        {
            LostPetService lostPetService = CreateLostPetService();
            var lostpets = lostPetService.GetLostPets();
            return Ok(lostpets);
        }
        public IHttpActionResult Get(int petID)
        {
            LostPetService lostPetService = CreateLostPetService();
            var lostpet = lostPetService.GetLostPetByID(petID);
            return Ok(lostpet);
        }

        
        public IHttpActionResult Get(string name)
        {
            LostPetService lostPetService = CreateLostPetService();
            var lostPetOwner = lostPetService.GetPetOwner(name);
            return Ok(lostPetOwner);
        }

        public IHttpActionResult Put(LostPetEdit lostPet)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateLostPetService();

            if (!service.UpdateByID(lostPet))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Delete(int ID)
        {
            var service = CreateLostPetService();

            if (!service.DeleteByID(ID))
                return InternalServerError();

            return Ok();
        }
    }
}
