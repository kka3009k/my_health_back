using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyHealth.Api.Service;
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
        private HttpContextService _contextService;

        public UserController(ILogger<UserController> logger, MyDbContext pDb, HttpContextService pContextService)
        {
            _logger = logger;
            _db = pDb;
            _contextService = pContextService;
        }

        /// <summary>
        /// Получить данные пользователя
        /// </summary>
        /// <returns></returns>
        [HttpGet("me")]
        [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetUser()
        {
            var userId = _contextService.UserId();
            var user = await _db.Users.FirstOrDefaultAsync(f => f.ID == userId);

            if (user == null)
                return BadRequest("Пользователь не найден");

            return Ok(new UserDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone,
                RhFactor = user.RhFactor,
                BirthDate = user.BirthDate,
                Address = user.Address,
                Blood = user.Blood,
                Gender = user.Gender,
                INN = user.INN,
                CreatedAt = user.CreatedAt,
            });
        }

        /// <summary>
        /// Обновить данные пользователя
        /// </summary>
        /// <param name="pUser"></param>
        /// <returns></returns>
        [HttpPost("update")]
        [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateUser([FromBody] UserDto pUser)
        {
            return Ok(pUser);
        }
    }
}