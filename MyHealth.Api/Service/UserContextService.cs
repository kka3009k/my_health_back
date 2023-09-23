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
            var header = headers.FirstOrDefault(a => a.Key.ToLower() == Constants.UserIdHeaderLower);

            if (!string.IsNullOrEmpty(header.Value))
            {
                var userId = int.Parse(header.Value);
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
