using Newtonsoft.Json;

namespace MyHealth.Api.Service
{
    public class HttpContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HttpContextService(IHttpContextAccessor pHttpContextAccessor)
        {
            _httpContextAccessor = pHttpContextAccessor;
        }
        public void GetUser()
        {
            throw new NotImplementedException();
        }

        public int UserId()
        {
            var user = _httpContextAccessor.HttpContext?.User?.Claims.FirstOrDefault(f => f.Type == "UserId");
            return user != null ? int.Parse(user.Value) : 0;
        }

        public void GetUserName()
        {
            throw new NotImplementedException();
        }
    }
}
