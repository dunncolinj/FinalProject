using FinalProject.Models;
using FinalProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FinalProject.WebAPI.Controllers
{
    public class LostPetController : ApiController
    {
        [Authorize]

        public IHttpActionResult Get(int id)
        {
            LostPetService lostPetService = CreateLostPetService();
            var lostPet = lostPetService.GetLostPets();//(id)
            return Ok(lostPet);
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

        private LostPetService CreateLostPetService()
        {
            var lostPetID = int.Parse(User.Identity.GetLostPetID());
            var lostPetService = new LostPetService(lostPetID);
            return lostPetService;
        }

        public IHttpActionResult Get()
        {
            LostPetService lostPetService = CreateLostPetService();
            var note = lostPetService.GetLostPets();
            return Ok(note);
        }
        public IHttpActionResult Get(int petID, int ownerID)
        {
            LostPetService lostPetService = CreateLostPetService();
            var note = lostPetService.GetLostPetByID(petID, ownerID);
            return Ok(note);
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

        public IHttpActionResult Delete(int petID)
        {
            var service = CreateLostPetService();

            if (!service.DeleteByID(petID))
                return InternalServerError();

            return Ok();
        }
    }
}
