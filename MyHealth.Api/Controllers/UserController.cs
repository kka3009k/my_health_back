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
        private UserContextService _contextService;
        private readonly FileStorageService _fileStorageService;

        public UserController(ILogger<UserController> logger, MyDbContext pDb, UserContextService pContextService, FileStorageService pFileStorageService)
        {
            _logger = logger;
            _db = pDb;
            _contextService = pContextService;
            _fileStorageService = pFileStorageService;
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
            var user = _contextService.User();

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

            return Ok(await LoadUser(user.ID));
        }

        private async Task<UserDto> LoadUser(int pUserID)
        {
            var user = await _db.Users.FirstOrDefaultAsync(f => f.ID == pUserID);
            var userDto = new UserDto()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Patronymic = user.Patronymic,
                Email = user.Email,
                Phone = user.Phone,
                RhFactor = user.RhFactor,
                BirthDate = user.BirthDate,
                Address = user.Address,
                Blood = user.Blood,
                Gender = user.Gender,
                INN = user.INN,
            };

            userDto.AvatarUrl = await _fileStorageService.GetFilePathAsync(user.AvatarFileID);
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
            var user = _contextService.User();
            var file = await _fileStorageService.SaveFileAsync(pFile);

            user.AvatarFileID = file.ID;
            await _db.SaveChangesAsync();
            var path = _fileStorageService.GetFilePath(file);
            return Ok(path);
        }
    }
}