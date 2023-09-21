using MyHealth.Api.Static;
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
            var headers = _httpContextAccessor.HttpContext?.Request?.Headers;

            if (headers.Any(a => a.Key == Constants.UserIdHeaderLower))
            {
                var user = _httpContextAccessor.HttpContext?.Request?.Headers[Constants.UserIdHeaderLower];
                var userId = int.Parse(user.Value);
                var hasUser = _db.Users.Any(f => f.ID == userId);

                if (hasUser)
                    return userId;

                return userId;
            }

            throw new UnauthorizedAccessException("User not found");
        }

        public void GetUserName()
        {
            throw new NotImplementedException();
        }
    }
}
