using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MyHealth.Data.Seed
{
    public static class MyContextSeed
    {
        private static MyDbContext _ctx;

        public static async Task SeedAsync(this MyDbContext pCtx)
        {
            _ctx = pCtx;
            var db =  pCtx.Database;
            await db.MigrateAsync();
        }

        //private static BfAbsContext CreateDb()
        //{
        //    var connectionString = _ctx.Database.GetConnectionString();
        //    if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(nameof(connectionString));

        //    var context = new BfAbsContext(connectionString, _loggerFactory);
        //    context.Database.SetCommandTimeout(0);
        //    return context;
        //}
    }
}