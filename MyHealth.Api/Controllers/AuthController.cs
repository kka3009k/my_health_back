using Microsoft.AspNetCore.Mvc;
using MyHealth.Data.Entities;

namespace MyHealth.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;

        public AuthController(ILogger<AuthController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public User Get()
        {
            return new User() { ID = 1 };
        }

        [HttpPost]
        public User Post([FromBody] User pUser)
        {
            return new User() { ID = 1 };
        }
    }
}