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

        #region Ovveride methodes

        public override int SaveChanges()
        {
            UpdateTime();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateTime();
            return base.SaveChangesAsync(cancellationToken);
        }

        #endregion

        #region DbSets

        /// <summary>
        /// Пользователи
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Данные верификации телефона
        /// </summary>
        public DbSet<PhoneVerificationData> PhoneVerificationData { get; set; }

        public DbSet<Metric> Metrics { get; set; }

        #endregion

        #region Private methodes

        private void UpdateTime()
        {
            var modifiedEntities = ChangeTracker.Entries().Where(e => e.State == EntityState.Modified);

            foreach (var entity in modifiedEntities)
            {
                if (entity.Entity is EntityBase entityBase)
                    entityBase.UpdatedAt = DateTime.UtcNow;
            }
        }

        #endregion
    }
}
