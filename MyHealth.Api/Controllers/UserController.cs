using DotNetEnv;
using Firebase.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyHealth.Api.Extension;
using MyHealth.Api.Service;
using MyHealth.Api.Static;
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

            return Ok(await LoadUser(userId));
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

            user.FillField(user.Email, pUser.Email, v => user.Email = v);
            user.FillField(user.Phone, pUser.Phone, v => user.Phone = v);
            user.FillField(user.FirstName, pUser.FirstName, v => user.FirstName = v);
            user.FillField(user.LastName, pUser.LastName, v => user.LastName = v);
            user.FillField(user.Patronymic, pUser.Patronymic, v => user.Patronymic = v);
            user.FillField(user.INN, pUser.INN, v => user.INN = v);
            user.FillField(user.Address, pUser.Address, v => user.Address = v);
            user.FillField(user.Gender, pUser.Gender, v => user.Gender = v);
            user.FillField(user.Blood, pUser.Blood, v => user.Blood = v);
            user.FillField(user.RhFactor, pUser.RhFactor, v => user.RhFactor = v);
            user.FillField(user.BirthDate, pUser.BirthDate, v => user.BirthDate = v);

            await _db.SaveChangesAsync();

            return Ok(await LoadUser(userId));
        }

        private async Task<UserDto> LoadUser(int pUserID)
        {
            var user = await _db.Users.FirstOrDefaultAsync(f => f.ID == pUserID);
            var userDto = new UserDto()
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
            };

            userDto.AvatarUrl = await GetFilePath(user.AvatarFileID);
            return userDto;
        }

        /// <summary>
        /// Загрузить фото профиля
        /// </summary>
        /// <remarks>
        /// Вернет путь к файлу
        /// </remarks>
        /// <param name="pFile">Файл</param>
        /// <returns></returns>
        [HttpPost("update/avatar")]
        public async Task<IActionResult> UpdateUserAvatar(IFormFile pFile)
        {
            var userId = _contextService.UserId();
            var user = await _db.Users.FirstOrDefaultAsync(f => f.ID == userId);

            if (user == null)
                return BadRequest("Пользователь не найден");

            var fileInfo = pFile.FileName.Split('.');
            var file = new FileStorage
            {
                Name = fileInfo[0],
                Extension = fileInfo.Length > 1 ? fileInfo[fileInfo.Length - 1] : string.Empty,
            };

            await _db.AddAsync(file);
            await _db.SaveChangesAsync();

            using (var stream = new FileStream(Path.Combine(Constants.FileStoragePath, $"{file.ID}.{file.Extension}"), FileMode.Create))
            {
                //copy the contents of the received file to the newly created local file 
                await pFile.CopyToAsync(stream);
            }

            user.AvatarFileID = file.ID;
            await _db.SaveChangesAsync();
            var path = await GetFilePath(file.ID);
            return Ok(path);
        }

        private async Task<string> GetFilePath(int? pFileID)
        {
            if (pFileID == null)
                return string.Empty;

            var file = await _db.FileStorages.FirstOrDefaultAsync(f => f.ID == pFileID);

            if (file == null)
                return string.Empty;

            return  $"{Constants.FileStorageName}/{file.ID}.{file.Extension}";
        }
    }
}