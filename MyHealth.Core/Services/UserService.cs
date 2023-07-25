using MyHealth.Data;
using MyHealth.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHealth.Core.Services
{
    public class UserService
    {
        private readonly MyDbContext _ctx;

        public UserService(MyDbContext pCtx)
        {
            _ctx = pCtx;
        }

        public async Task<User>
    }
}
