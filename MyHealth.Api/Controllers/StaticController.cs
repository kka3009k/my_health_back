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

        /// <summary>
        /// Assetlinks
        /// </summary>
        /// <returns></returns>
        [HttpGet(".well-known/assetlinks.json")]
        public async Task<ContentResult> Assetlinks()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Static", "assetlinks.json");
            var html = await new StreamReader(path).ReadToEndAsync();
            return Content(html, "application/json");
        }

        /// <summary>
        /// Apple app site association
        /// </summary>
        /// <returns></returns>
        [HttpGet(".well-known/apple-app-site-association")]
        public async Task<ContentResult> AppleAssociation()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Static", "apple-app-site-association");
            var html = await new StreamReader(path).ReadToEndAsync();
            return Content(html, "application/json");
        }
    }
}