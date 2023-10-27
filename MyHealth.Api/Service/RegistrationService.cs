using MyHealth.Data;
using MyHealth.Data.Dto;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using MyHealth.Data.Entities;
using MyHealth.Api.Utils;

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
            var otp = $"H-{Random.Shared.Next(1000, 9999)}";

            await _mailService.SendAsync("Подтверждение регистрации", 
                $"Код ОТП для подтверждения регистрации: {otp}", 
                new MailboxAddress("MyHealthClient", pPar.Email));

            OtpProvider.SetOtp(pPar.Email, otp);
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
            user.PasswordHash = Сryptography.ComputeSha256Hash(pPar.Password);
            await _db.SaveChangesAsync();
            var res = _authService.GenerateToken(user);
            return res;
        }
    }
}
