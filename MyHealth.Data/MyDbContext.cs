using Microsoft.EntityFrameworkCore;
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
    }
}
