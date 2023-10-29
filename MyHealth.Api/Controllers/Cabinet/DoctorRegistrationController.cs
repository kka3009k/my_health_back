using Microsoft.AspNetCore.Mvc;
using MyHealth.Api.Utils;
using MyHealth.Data;
using MyHealth.Data.Dto;
using MyHealth.Data.Entities;
using MyHealth.Data.Entities.DoctorCabinet;

namespace MyHealth.Api.Controllers.Cabinet
{
    [ApiController]
    [Route("[controller]")]
    public class DoctorRegistrationController : Controller
    {
        private readonly MyDbContext _db;

        public IActionResult Registration(DoctorUserDto pUser)
        {
            var userExists = _db.Users.Any(a => a.Phone == pUser.Phone);

            if (userExists)
                return BadRequest("Пользователь существует");


            return View();
        }

        public async Task<IActionResult> DoctorRegistration(DoctorUserDto pUser)
        {
            var user = new User
            {
                FirstName = pUser.FirstName,
                LastName = pUser.LastName,
                Role = RoleTypes.Doctor,
                Email = pUser.Email,
                Phone = pUser.Phone,
                //PasswordHash = Cryptography.ComputeSha256Hash(pUser.Password)

            };

            await _db.AddAsync(user);
            await _db.SaveChangesAsync();

            return Ok();
        }
    }
}
