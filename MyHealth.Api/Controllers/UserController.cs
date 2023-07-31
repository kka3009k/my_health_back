using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyHealth.Data;
using MyHealth.Data.Dto;
using MyHealth.Data.Entities;
using System.Net;

namespace MyHealth.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly MyDbContext _db;

        public UserController(ILogger<UserController> logger, MyDbContext pDb)
        {
            _logger = logger;
            _db = pDb;
        }

        /// <summary>
        /// Получить данные пользователя
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("me")]
        [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetUser(int? userId)
        {
            var user = await _db.Users.FirstOrDefaultAsync(f => f.ID == userId);

            if (user == null)
                return BadRequest("Пользователь не найден");

            return Ok(new UserDto
            {
                UserID = userId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone,
                Role = user.Role,
            });
        }

        [HttpPost("update")]
        public User UpdateUser([FromBody] User pUser)
        {
            return new User() { ID = 1 };
        }
    }
}