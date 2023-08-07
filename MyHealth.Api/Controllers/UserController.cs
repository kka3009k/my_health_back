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
            var userId = _contextService.UserId();
            var user = await _db.Users.FirstOrDefaultAsync(f => f.ID == userId);

            if (user == null)
                return BadRequest("Пользователь не найден");

            FillField(user.Email, pUser.Email, v => user.Email = v);
            FillField(user.Phone, pUser.Phone, v => user.Phone = v);
            FillField(user.FirstName, pUser.FirstName, v => user.FirstName = v);
            FillField(user.LastName, pUser.LastName, v => user.LastName = v);
            FillField(user.Patronymic, pUser.Patronymic, v => user.Patronymic = v);
            FillField(user.INN, pUser.INN, v => user.INN = v);
            FillField(user.Address, pUser.Address, v => user.Address = v);
            FillField(user.Gender, pUser.Gender, v => user.Gender = v);
            FillField(user.Blood, pUser.Blood, v => user.Blood = v);
            FillField(user.RhFactor, pUser.RhFactor, v => user.RhFactor = v);
            FillField(user.BirthDate, pUser.BirthDate, v => user.BirthDate = v);

            await _db.SaveChangesAsync();

            return Ok(pUser);
        }

        private void FillField(string pOld, string pNew, Action<string> pAction)
        {
            if (!string.IsNullOrWhiteSpace(pNew) && (!pOld?.Equals(pNew) ?? true))
                pAction(pNew);
        }

        private void FillField(DateTime? pOld, DateTime? pNew, Action<DateTime?> pAction)
        {
            if (pNew != null && (!pOld?.Equals(pNew) ?? false))
                pAction(pNew);
        }

        private void FillField<T>(T pOld, T pNew, Action<T> pAction)
        {
            if (pNew != null && !pOld.Equals(pNew))
                pAction(pNew);
        }


    }
}