using MyHealth.Api.Static;
using MyHealth.Data;
using MyHealth.Data.Entities;

namespace MyHealth.Api.Service
{
    /// <summary>
    /// Читатель данные пользователя из контекста запроса
    /// </summary>
    public class UserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly MyDbContext _db;

        public UserContextService(IHttpContextAccessor pHttpContextAccessor, MyDbContext pDb)
        {
            _httpContextAccessor = pHttpContextAccessor;
            _db = pDb;
        }

        /// <summary>
        /// Получить текущего пользователя
        /// </summary>
        /// <param name="pByToken">Поиск по токену</param>
        /// <returns></returns>
        public User User(bool pByToken = false)
        {
            var userId = UserId(pByToken);
            var user = _db.Users.FirstOrDefault(f => f.ID == userId);
            return user;
        }

        /// <summary>
        /// Получить код текущего пользователя
        /// </summary>
        /// <param name="pByToken">Поиск по токену</param>
        /// <returns></returns>
        public Guid UserId(bool pByToken = false)
        {
            var userId = string.Empty;

            if (pByToken)
                userId = _httpContextAccessor.HttpContext?.User?.Claims.FirstOrDefault(f => f.Type == "UserId")?.Value;
            else
                userId = _httpContextAccessor.HttpContext?.Request?.Headers?
                    .FirstOrDefault(a => a.Key.ToLower() == Constants.UserIdHeaderLower).Value;

            if (Guid.TryParse(userId, out Guid id))
            {
                var hasUser = _db.Users.Any(f => f.ID == id);

                if (hasUser)
                    return id;
            }

            throw new UnauthorizedAccessException("User not found");
        }

        public void GetUserName()
        {
            throw new NotImplementedException();
        }

        public bool IsMain(Guid pUserId)
        {
            var userId = UserId(true);
            return pUserId == userId;
        }
    }
}
