using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyHealth.Data;
using MyHealth.Data.Dto;
using MyHealth.Data.Entities;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace MyHealth.Api.Controllers
{
    /*[ApiController]
    [Route("[controller]")]
    public class RegistrationController : ControllerBase
    {
        private readonly ILogger<RegistrationController> _logger;
        private readonly MyDbContext _db;

        public RegistrationController(ILogger<RegistrationController> logger, MyDbContext pDb)
        {
            _logger = logger;
            _db = pDb;
        }

        /// <summary>
        /// Регистрация нового пользователя
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Registration(UserRegistrationDto pUser)
        {
            var userExists = await _db.Users.AnyAsync(a => a.Phone == pUser.Phone);

            if (userExists)
                return BadRequest("Пользователь существует");

            var user = new User
            {
                FirstName = pUser.FirstName,
                LastName = pUser.LastName,
                Role = RoleTypes.Client,
                Email = pUser.Email,
                Phone = pUser.Phone,
                PasswordHash = ComputeSha256Hash(pUser.Password)
            };

            await _db.AddAsync(user);
            await _db.SaveChangesAsync();

            return Ok();
        }

        private string ComputeSha256Hash(string pRawData)
        {
            // Create a SHA256
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(pRawData));

                // Convert byte array to a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }*/
}