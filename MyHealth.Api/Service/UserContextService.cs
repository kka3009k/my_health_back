using MyHealth.Data;
using MyHealth.Data.Entities;
using Newtonsoft.Json;

namespace MyHealth.Api.Service
{
    public class UserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly MyDbContext _db;

        public UserContextService(IHttpContextAccessor pHttpContextAccessor, MyDbContext pDb)
        {
            _httpContextAccessor = pHttpContextAccessor;
            _db = pDb;
        }
        public User User()
        {
            var userId = UserId();
            var user = _db.Users.FirstOrDefault(f => f.ID == userId);
            return user;
        }

        public int UserId()
        {
            var user = _httpContextAccessor.HttpContext?.User?.Claims.FirstOrDefault(f => f.Type == "UserId");
            var userId = user != null ? int.Parse(user.Value) : 0;
            var hasUser = _db.Users.Any(f => f.ID == userId);

            if (!hasUser)
            {
                throw new UnauthorizedAccessException("User not found");
            }

            return userId;
        }

        public void GetUserName()
        {
            throw new NotImplementedException();
        }
    }
}
