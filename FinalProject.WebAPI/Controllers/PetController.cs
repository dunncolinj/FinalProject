using FinalProject.Models;
using FinalProject.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace FinalProject.WebAPI.Controllers
{
        [Authorize]
        public class PetController : ApiController
        {
            public IHttpActionResult Get()
            {
                PetService petService = CreatePetService();
                var pets = petService.GetPets();
                return Ok(pets);
            }

            public IHttpActionResult Get(int id)
            {
                PetService petService = CreatePetService();
                var pet = petService.GetPetByID(id);
                return Ok(pet);
            }

            public IHttpActionResult Post(PetCreate pet)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var service = CreatePetService();

                if (!service.CreatePet(pet))
                    return InternalServerError();

                return Ok();
            }

            public IHttpActionResult Put(PetEdit pet)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var service = CreatePetService();

                if (!service.UpdatePet(pet))
                    return InternalServerError();

                return Ok();
            }

            public IHttpActionResult Delete(int id)
            {
                var service = CreatePetService();

                if (!service.DeletePet(id))
                    return InternalServerError();

                return Ok();
            }

            private PetService CreatePetService()
            {
                var userId = int.Parse(User.Identity.GetUserId());
                var petService = new PetService(userId);
                return petService;
            }

        }
    }