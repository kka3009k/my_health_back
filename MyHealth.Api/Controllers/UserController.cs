using Microsoft.AspNetCore.Mvc;
using MyHealth.Data.Entities;

namespace MyHealth.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
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