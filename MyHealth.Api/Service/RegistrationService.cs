using Microsoft.EntityFrameworkCore;
using MyHealth.Api.Utils;
using MyHealth.Data;
using MyHealth.Data.Dto;
using MyHealth.Data.Entities;

namespace MyHealth.Api.Service
{
    public class RegistrationService
    {
        private readonly MyDbContext _db;
        private AuthService _authService;
        private MailService _mailService;

        public RegistrationService(MyDbContext pDb, AuthService pAuthService, MailService pMailService)
        {
            _db = pDb;
            _authService = pAuthService;
            _mailService = pMailService;
        }

        public async Task RegistrationAsync(RegistrationPar pPar)
        {
            if (await _db.Users.AnyAsync(a => a.Email == pPar.Email))
                throw new Exception("С такой почтой пользователь уже существует");

            var otp = OtpProvider.GenerateOtp(pPar.Email);

            await _mailService.SendAsync("Подтверждение регистрации",
                $"Код ОТП для подтверждения регистрации: {otp}",
                pPar.Email);
        }

        public async Task<AuthResDto> ConfirmRegistrationAsync(ConfirmRegistrationPar pPar)
        {
            if (OtpProvider.HasOtp(pPar.Email, pPar.Otp))
                OtpProvider.RemoveOtp(pPar.Email);
            else
                throw new Exception("Неверный код ОТП");

            var user = await _db.Users.FirstOrDefaultAsync(f => f.Email == pPar.Email && !string.IsNullOrEmpty(pPar.Email));

            if (user == null)
            {
                user = new User();
                user.Email = pPar.Email;
                await _db.Users.AddAsync(user);
            }

            user.Role = RoleTypes.Client;
            user.PasswordHash = Cryptography.ComputeSha256Hash(pPar.Password);
            await _db.SaveChangesAsync();
            var res = _authService.GenerateToken(user);
            return res;
        }
    }
}
