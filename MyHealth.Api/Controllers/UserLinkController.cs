using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyHealth.Api.Service;
using MyHealth.Api.Static;
using MyHealth.Data;
using MyHealth.Data.Dto;
using MyHealth.Data.Entities;
using System.Net;

namespace MyHealth.Api.Controllers
{
    /// <summary>
    /// Связанные аккаунты пользователя
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class UserLinkController : ControllerBase
    {
        private readonly ILogger<UserLinkController> _logger;
        private readonly MyDbContext _db;
        private UserContextService _contextService;

        public UserLinkController(ILogger<UserLinkController> logger, MyDbContext pDb, UserContextService pContextService)
        {
            _logger = logger;
            _db = pDb;
            _contextService = pContextService;
        }
        /// <summary>
        /// Добавить доп. профиль
        /// </summary>
        /// <param name="pUserLink">Данные добавдения доп. профиля</param>
        /// <returns></returns>
        [HttpPost("add")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddLink(AddUserLinkDto pUserLink)
        {
            var res = await CreateLink(pUserLink);
            return Ok(res);
        }

        /// <summary>
        /// Получить связи пользователя
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<UserLinkDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetLinks()
        {
            var user = _contextService.User(true);
            var userLinks = await (
                from link in _db.UserLinks
                join secondaryUser in _db.Users on link.SecondaryUserID equals secondaryUser.ID
                join avatar in _db.FileStorages on secondaryUser.AvatarFileID equals avatar.ID into avatars
                from avatar in avatars.DefaultIfEmpty()
                where link.MainUserID == user.ID
                select new
                {
                    SecondaryUser = secondaryUser,
                    link.UserLinkTypeID,
                    AvatarUrl = avatar != null ? $"{Constants.FileStorageName}/{avatar.ID}.{avatar.Extension}" : null
                }
                ).ToListAsync();

            var profiles = userLinks.Select(s => new UserLinkDto
            {
                UserID = s.SecondaryUser.ID,
                FullName = s.SecondaryUser.FullName,
                UserLinkTypeID = s.UserLinkTypeID,
                AvatarUrl = s.AvatarUrl,
            }).ToList();

            profiles.Add(new UserLinkDto
            {
                UserID = user.ID,
                FullName = user.FullName,
                AvatarUrl = await new FileStorageService(_db).GetFilePathAsync(user.AvatarFileID),
                IsMain = true
            });

            return Ok(profiles);
        }

        /// <summary>
        /// Удалить связь пользователя
        /// </summary>
        /// <param name="pSecondaryUserID">Код доп. пользователя</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteLink(Guid pSecondaryUserID)
        {
            var userId = _contextService.UserId(true);
            var userLink = await _db.UserLinks
                .FirstOrDefaultAsync(f => f.SecondaryUserID == pSecondaryUserID
                    && f.MainUserID == userId);

            if (userLink == null)
                return BadRequest("Связь не найдена");

            _db.Remove(userLink);
            await _db.SaveChangesAsync();

            return Ok();
        }

        private async Task<Guid> CreateLink(AddUserLinkDto pUserLink)
        {
            var mainUserID = _contextService.UserId(true);
            var userLink = await _db.UserLinks
                .FirstOrDefaultAsync(f => f.MainUserID == mainUserID
                && (pUserLink.FirstName == null || f.SecondaryUser.FirstName == pUserLink.FirstName.Trim())
                && (pUserLink.LastName == null || f.SecondaryUser.LastName == pUserLink.LastName.Trim())
                && (pUserLink.Patronymic == null || f.SecondaryUser.Patronymic == pUserLink.Patronymic.Trim())
                );

            if (userLink == null)
            {
                var user = new User();
                user.FirstName = pUserLink.FirstName;
                user.LastName = pUserLink.LastName;
                user.Patronymic = pUserLink.Patronymic;
                user.Role = RoleTypes.Client;
                await _db.Users.AddAsync(user);

                userLink = new UserLink() { SecondaryUser = user };
                await _db.UserLinks.AddAsync(userLink);
            }

            userLink.MainUserID = mainUserID;
            userLink.UserLinkTypeID = pUserLink.UserLinkTypeID;

            await _db.SaveChangesAsync();

            return userLink.SecondaryUserID;
        }
    }
}