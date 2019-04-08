using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataLayer;
using DataLayer.Entities;
using Marks.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Marks.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private EFDBContext context;
        public UserController(EFDBContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] UserToCreateDto user)
        {
            if (user == null)
                return BadRequest();

            if (!string.IsNullOrEmpty(user.Login) &&
                !user.Login.All(char.IsLetterOrDigit))
            {
                ModelState.AddModelError(nameof(UserToCreateDto.Login),
                    "Login should contain only letters or digits.");
            }

            if (!ModelState.IsValid)
                return new UnprocessableEntityObjectResult(ModelState);

            var userEntity = Mapper.Map<User>(user);
            var createdUserEntity = context.Users.Add(userEntity);
            context.SaveChanges();

            return CreatedAtRoute(
                new { userId = createdUserEntity.Entity.Id },
                createdUserEntity.Entity.Id);
        }
    }
}
