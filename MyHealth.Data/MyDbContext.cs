using Microsoft.EntityFrameworkCore;
using MyHealth.Data.Entities;
using MyHealth.Data.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHealth.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> pOptions) : base(pOptions) { }

        #region Model configure

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigEntities();
        }

        #endregion

        #region DbSets

        /// <summary>
        /// Пользователи
        /// </summary>
        public DbSet<User> Users { get; set; }
        
        /// <summary>
        /// Данные авторизации
        /// </summary>
        public DbSet<UserLoginData> UserLoginData { get; set; }

        /// <summary>
        /// Данные верификации телефона
        /// </summary>
        public DbSet<PhoneVerificationData> PhoneVerificationData { get; set; }

        #endregion
    }
}
