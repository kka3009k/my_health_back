using Microsoft.AspNetCore.Mvc;

namespace MyHealth.Api.Controllers
{
    [ApiController]
    [Route("status_check")]
    public class StatusController : ControllerBase
    {
        private readonly ILogger<StatusController> _logger;

        public StatusController(ILogger<StatusController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Получить соответствующий статус
        /// </summary>
        /// <returns></returns>
        [HttpGet("{code}")]
        [HttpPost("{code}")]
        public IActionResult GetStatusResult(int code)
        {
            return StatusCode(code, $"request status: {code}");
        }


    }
}