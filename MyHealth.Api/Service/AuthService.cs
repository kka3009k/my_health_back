using DotNetEnv;
using FirebaseAdmin.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyHealth.Api.Utils;
using MyHealth.Data;
using MyHealth.Data.Dto;
using MyHealth.Data.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Firebase_Auth = FirebaseAdmin.Auth.FirebaseAuth;

namespace MyHealth.Api.Service
{
    public class AuthService
    {
        private readonly MyDbContext _db;
        private UserContextService _contextService;
        private MailService _mailService;

        public AuthService(MyDbContext pDb, UserContextService pContextService, MailService pMailService)
        {
            _db = pDb;
            _contextService = pContextService;
            _mailService = pMailService;
        }
        public async Task<AuthResDto> FirebaseAuthAsync(string token)
        {
            var fbToken = await Firebase_Auth.DefaultInstance.VerifyIdTokenAsync(token);
            var user = await Firebase_Auth.DefaultInstance.GetUserAsync(fbToken.Uid);
            var res = await GenerateTokenFirebase(user);
            return res;
        }


        public async Task<AuthResDto> FirebaseAuthUidAsync(string uid)
        {
            var user = await Firebase_Auth.DefaultInstance.GetUserAsync(uid);
            var res = await GenerateTokenFirebase(user);
            return res;
        }

        public async Task<AuthResDto> LoginAuthAsync(AuthPar pAuthPar)
        {
            var user = await _db.Users
                .FirstOrDefaultAsync(f => (pAuthPar.Login.Contains("@") && f.Email == pAuthPar.Login) || f.Phone == pAuthPar.Login);

            if (user != null)
            {
                if (Cryptography.ValidPassword(pAuthPar.Password, user.PasswordHash))
                {
                    var res = GenerateToken(user);
                    return res;
                }

                throw new Exception("Неверный пароль");
            }

            throw new Exception("Пользователь не найден");
        }


        public Task<AuthResDto> RefreshTokenAsync(string refreshToken)
        {
            new JwtSecurityTokenHandler().ValidateToken(refreshToken, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Env.GetString("SECRET_KEY"))),
                ValidateIssuer = false,
                ValidateAudience = false
            }, out SecurityToken validatedToken);

            var user = _contextService.User();

            if (user == null)
                throw new Exception("Пользователь не найден");

            var res = GenerateToken(user);
            return Task.FromResult(res);
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

        public AuthResDto GenerateToken(User pUser)
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
                    AccessToken = accessToken,
                    AccessTokenExpiresAt = expiresAccessToken,
                    RefreshToken = refreshToken,
                    RefreshTokenExpiresAt = expiresRefreshToken,
                    UserID = pUser.ID
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($@"GenerateToken -> {ex.Message}");
                throw;
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
                    new("Role", pUser.Role.ToString()),
                };

            var claimsIdentity =
            new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }

        private bool IsDifferent(string pOld, string pNew)
        {
            return !string.IsNullOrWhiteSpace(pNew) && (!pOld?.Equals(pNew) ?? true);
        }

        public async Task ResetPasswordAsync(string pEmail)
        {
            var user = await _db.Users.FirstOrDefaultAsync(f => f.Email == pEmail);

            if (user == null)
                throw new Exception("Пользователь не найден");

            var otp = OtpProvider.GenerateOtp(pEmail);
            await _mailService.SendAsync("Подтверждение сброса пароля", $"Код ОТП для подтверждения сброса пароля: {otp}", pEmail);
        }

        public async Task ConfirmResetPasswordAsync(ConfirmResetPasswordPar pConfirmResetPasswordPar)
        {
            var user = await _db.Users.FirstOrDefaultAsync(f => f.Email == pConfirmResetPasswordPar.Email);

            if (user == null)
                throw new Exception("Пользователь не найден");

            if (OtpProvider.HasOtp(pConfirmResetPasswordPar.Email, pConfirmResetPasswordPar.Otp))
            {
                user.PasswordHash = Cryptography.ComputeSha256Hash(pConfirmResetPasswordPar.NewPassword);
                await _db.SaveChangesAsync();
                OtpProvider.RemoveOtp(pConfirmResetPasswordPar.Email);
                return;
            }

            throw new Exception("Неверный код ОТП");
        }
    }
}
