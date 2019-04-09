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

        [HttpPost("[action]")]
        public IActionResult Reg([FromBody] UserToCreateDto user)
        {
            if (user == null)
                return BadRequest();

            if (string.IsNullOrEmpty(user.Login) &&
                !user.Login.All(char.IsLetterOrDigit))
            {
                ModelState.AddModelError(nameof(UserToCreateDto.Login),
                    "Login should contain only letters or digits.");
            }

            if (!ModelState.IsValid)
                return new UnprocessableEntityObjectResult(ModelState);

            var userEntity = Mapper.Map<User>(user);
            var loginsUsers = context.Users.Select(x => x.Login).ToList();
            if (loginsUsers.Contains(userEntity.Login))
                return BadRequest(); //Нужно возвращать что-то нормальное, а не бедреквест
            var createdUserEntity = context.Users.Add(userEntity);
            context.SaveChanges();

            return CreatedAtRoute(
                new { userId = createdUserEntity.Entity.Id },
                createdUserEntity.Entity.Id);
        }

        [HttpPost("[action]")]
        public IActionResult Auth([FromBody] UserToCreateDto user)
        {
            if (user == null)
                return BadRequest();

            if (string.IsNullOrEmpty(user.Login) &&
                !user.Login.All(char.IsLetterOrDigit))
            {
                ModelState.AddModelError(nameof(UserToCreateDto.Login),
                    "Login should contain only letters or digits.");
            }

            if (!ModelState.IsValid)
                return new UnprocessableEntityObjectResult(ModelState);

            var userEntity = Mapper.Map<User>(user);
            var userOnDb = context.Users.Where(x => x.Login == userEntity.Login).FirstOrDefault();
            if (userOnDb == null || userOnDb.Password != userEntity.Password)
                return BadRequest(); //Нужно возвращать что-то нормальное, а не бедреквест
            return CreatedAtRoute(
                new { userId = userOnDb.Id },
                userOnDb.Id);
        }
    }
}
