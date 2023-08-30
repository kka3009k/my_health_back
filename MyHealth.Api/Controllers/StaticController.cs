using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyHealth.Data;
using MyHealth.Data.Dto;
using MyHealth.Data.Entities;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace MyHealth.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StaticController : ControllerBase
    {
        private readonly ILogger<StaticController> _logger;
        private readonly MyDbContext _db;

        public StaticController(ILogger<StaticController> logger, MyDbContext pDb)
        {
            _logger = logger;
            _db = pDb;
        }

        /// <summary>
        /// Пользовательское соглашение
        /// </summary>
        /// <returns></returns>
        [HttpGet("terms_of_use")]
        public async Task<ContentResult> TermOfUse()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Static", "TermsOfUse.html");
            var html = await new StreamReader(path).ReadToEndAsync();
            return Content(html, "text/html");
        }
    }
}