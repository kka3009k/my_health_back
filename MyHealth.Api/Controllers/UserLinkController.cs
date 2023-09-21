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
        private readonly AuthService _authService;
        private readonly MyDbContext _db;
        private UserContextService _contextService;

        public UserLinkController(ILogger<UserLinkController> logger, AuthService pAuthService, MyDbContext pDb, UserContextService pContextService)
        {
            _logger = logger;
            _authService = pAuthService;
            _db = pDb;
            _contextService = pContextService;
        }

        /// <summary>
        /// Добавление через Firebase
        /// </summary>
        /// <param name="token">Токен firebase</param>
        /// <param name="userLinkTypeID">Тип связи с пользователем</param>
        /// <returns></returns>
        [HttpPost("add/firebase")]
        [ProducesResponseType(typeof(AuthResDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddLink(string token, int userLinkTypeID)
        {
            try
            {
                var res = await _authService.FirebaseAuthAsync(token);
                await CreateLink(res, userLinkTypeID);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Добавление через Firebase по Uid, использовать только для тестов
        /// </summary>
        /// <param name="uid">Индентификатор firebase</param>
        /// <param name="userLinkTypeID">Тип связи с пользователем</param>
        /// <returns></returns>
        [HttpPost("add/firebase/uid")]
        [ProducesResponseType(typeof(AuthResDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> FirebaseAuthUid(string uid, int userLinkTypeID)
        {
            try
            {
                var res = await _authService.FirebaseAuthUidAsync(uid);
                await CreateLink(res, userLinkTypeID);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest(ex.Message);
            }
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
            var userId = _contextService.UserId();
            var userLinks = await (
                from link in _db.UserLinks
                join secondaryUser in _db.Users on link.SecondaryUserID equals secondaryUser.ID
                join avatar in _db.FileStorages on secondaryUser.AvatarFileID equals avatar.ID into avatars
                from avatar in avatars.DefaultIfEmpty()
                where link.MainUserID == userId
                select new
                {
                    SecondaryUser = secondaryUser,
                    UserLinkTypeID = link.UserLinkTypeID,
                    AvatarUrl = avatar != null ? $"{Constants.FileStorageName}/{avatar.ID}.{avatar.Extension}" : null
                }
                ).ToListAsync();

            return Ok(userLinks.Select(s => new UserLinkDto
            {
                SecondaryUserID = s.SecondaryUser.ID,
                FullName = s.SecondaryUser.FullName,
                UserLinkTypeID = s.UserLinkTypeID,
                AvatarUrl = s.AvatarUrl,
            }).ToList());
        }

        private async Task CreateLink(AuthResDto pSecondaryUser, int pUserLinkTypeID)
        {
            var mainUserID = _contextService.UserId();
            var userLink = await _db.UserLinks
                .FirstOrDefaultAsync(f => f.MainUserID == mainUserID && f.SecondaryUserID == pSecondaryUser.UserID);

            if (userLink == null)
            {
                userLink = new UserLink();
                await _db.UserLinks.AddAsync(userLink);
            }

            userLink.MainUserID = mainUserID;
            userLink.SecondaryUserID = pSecondaryUser.UserID ?? default;
            userLink.UserLinkTypeID = pUserLinkTypeID;

            await _db.SaveChangesAsync();
        }
    }
}