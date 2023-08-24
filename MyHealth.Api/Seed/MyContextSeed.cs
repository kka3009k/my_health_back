using DotNetEnv;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyHealth.Api.Utils;
using MyHealth.Data;
using MyHealth.Data.Entities;
using System.Data;

namespace MyHealth.Api.Seed
{
    public static class MyContextSeed
    {
        private static MyDbContext _ctx;

        public static async Task SeedAsync(this MyDbContext pCtx)
        {
            _ctx = pCtx;
            await _ctx.Database.MigrateAsync();
            await AddAdminAsync();
        }

        //private static BfAbsContext CreateDb()
        //{
        //    var connectionString = _ctx.Database.GetConnectionString();
        //    if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(nameof(connectionString));

        //    var context = new BfAbsContext(connectionString, _loggerFactory);
        //    context.Database.SetCommandTimeout(0);
        //    return context;
        //}

        private static async Task AddAdminAsync()
        {
            var email = Env.GetString("ADMIN_EMAIL");
            var password = Env.GetString("ADMIN_PASSWORD");

            if (string.IsNullOrEmpty(email))
                return;

            var user = await _ctx.Users.FirstOrDefaultAsync(x => x.Email == email);

            if (user == null)
            {
                user = new User();
                await _ctx.AddAsync(user);
            };

            user.Email = email;
            user.PasswordHash = Сryptography.ComputeSha256Hash(password);
            user.FirstName = "admin";
            user.Role = RoleTypes.Admin;

            await _ctx.SaveChangesAsync();
        }
    }
}