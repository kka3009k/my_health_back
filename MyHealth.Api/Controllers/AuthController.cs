using DotNetEnv;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyHealth.Data;
using MyHealth.Data.Dto;
using MyHealth.Data.Entities;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using Firebase_Auth = FirebaseAdmin.Auth.FirebaseAuth;

namespace MyHealth.Api.Controllers
{
    /// <summary>
    /// Авторизация
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly MyDbContext _db;

        public AuthController(ILogger<AuthController> logger, MyDbContext pDb)
        {
            _logger = logger;
            _db = pDb;
        }

        /// <summary>
        /// Авторизация через Firebase
        /// </summary>
        /// <param name="token">Токен firebase</param>
        /// <returns></returns>
        [HttpPost("firebase")]
        [ProducesResponseType(typeof(AuthResDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> FirebaseAuth(string token)
        {
            var fbToken = await Firebase_Auth.DefaultInstance.VerifyIdTokenAsync(token);
            var pUser = await Firebase_Auth.DefaultInstance.GetUserAsync(fbToken.Uid);
            var res = await GenerateTokenFirebase(pUser);
            return Ok(res);
        }

        /// <summary>
        /// Авторизация через Firebase по Uid, использовать только для тестов
        /// </summary>
        /// <param name="uid">Индентификатор firebase</param>
        /// <returns></returns>
        [HttpPost("firebase/uid")]
        [ProducesResponseType(typeof(AuthResDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> FirebaseAuthUid(string uid)
        {
            var pUser = await Firebase_Auth.DefaultInstance.GetUserAsync(uid);
            var res = await GenerateTokenFirebase(pUser);
            return Ok(res);
        }

        /// <summary>
        /// Обновление токена
        /// </summary>
        /// <param name="refreshToken">Токен обновления</param>
        /// <returns></returns>
        [HttpPost("refresh")]
        [ProducesResponseType(typeof(AuthResDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> RefreshToken(string refreshToken)
        {
            new JwtSecurityTokenHandler().ValidateToken(refreshToken, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Env.GetString("SECRET_KEY"))),
                ValidateIssuer = false,
                ValidateAudience = false
            }, out SecurityToken validatedToken);

            var userId = int.Parse(((JwtSecurityToken)validatedToken).Claims.FirstOrDefault(f => f.Type == "UserId").Value);
            var user = await _db.Users.FirstOrDefaultAsync(f => f.ID == userId);

            if (user == null)
                return BadRequest("Пользователь не найден");

            var res = GenerateToken(user);
            return Ok(res);
        }


        private async Task<AuthResDto> GenerateTokenFirebase(UserRecord pUser)
        {
            var user = await _db.Users.FirstOrDefaultAsync(f => (f.Email == pUser.Email && !string.IsNullOrEmpty(pUser.Email))
            || (f.Phone == pUser.PhoneNumber && !string.IsNullOrEmpty(pUser.PhoneNumber)));

            if (user == null)
            {
                user = new User();
                user.Email = pUser.Email;
                user.Phone = pUser.PhoneNumber;
                user.FirstName = pUser.DisplayName?.Split(' ').LastOrDefault();
                user.LastName = pUser.DisplayName?.Split(' ').FirstOrDefault();
                await _db.Users.AddAsync(user);
            }
            else
            {
                if (IsDifferent(user.Email, pUser.Email))
                    user.Email = pUser.Email;

                if (IsDifferent(user.Phone, pUser.PhoneNumber))
                    user.Phone = pUser.PhoneNumber;
            }

            user.Role = RoleTypes.Client;
            user.FirebaseUid = pUser.Uid;
            await _db.SaveChangesAsync();
            var res = GenerateToken(user);
            return res;
        }

        private AuthResDto GenerateToken(User pUser)
        {
            var identity = GetIdentity(pUser);
            var now = DateTime.UtcNow;
            var expiresAccessToken = now.Add(TimeSpan.FromMinutes(Env.GetInt("LIFE_TIME_TOKEN")));
            var expiresRefreshToken = now.Add(TimeSpan.FromMinutes(Env.GetInt("LIFE_TIME_REFRESH_TOKEN")));

            try
            {
                var accessToken = new JwtSecurityTokenHandler().WriteToken(InitToken(identity, expiresAccessToken));
                var refreshToken = new JwtSecurityTokenHandler().WriteToken(InitToken(identity, expiresRefreshToken));

                return new AuthResDto
                {
                    UserID = pUser.ID,
                    AccessToken = accessToken,
                    AccessTokenExpiresAt = expiresAccessToken,
                    RefreshToken = refreshToken,
                    RefreshTokenExpiresAt = expiresRefreshToken
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($@"GenerateToken -> {ex.Message}");
                return null;
            }
        }

        private JwtSecurityToken InitToken(ClaimsIdentity pIdentity, DateTime pExpires)
        {

            return new JwtSecurityToken(
                         issuer: Env.GetString("ISSUER_TOKEN"),
                         audience: Env.GetString("AUDIENCE_TOKEN"),
                         notBefore: DateTime.UtcNow,
                         claims: pIdentity.Claims,
                         expires: pExpires,
                         signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Env.GetString("SECRET_KEY"))), SecurityAlgorithms.HmacSha256));
        }

        private ClaimsIdentity GetIdentity(User pUser)
        {
            var claims = new List<Claim>
                {
                    new("UserName", pUser.UserName),
                    new("UserId", pUser.ID.ToString()),
                };

            var claimsIdentity =
            new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }

        private bool IsDifferent(string pOld, string pNew)
        {
            return !string.IsNullOrWhiteSpace(pNew) && !pOld.Equals(pNew);
        }
    }
}