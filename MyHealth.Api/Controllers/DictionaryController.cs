using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyHealth.Api.Service;
using MyHealth.Data;
using MyHealth.Data.Dto;
using MyHealth.Data.Entities;
using System.Net;

namespace MyHealth.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DictionaryController : ControllerBase
    {
        private readonly ILogger<DictionaryController> _logger;
        private readonly MyDbContext _db;
        private HttpContextService _contextService;

        public DictionaryController(ILogger<DictionaryController> logger, MyDbContext pDb, HttpContextService pContextService)
        {
            _logger = logger;
            _db = pDb;
            _contextService = pContextService;
        }

        /// <summary>
        /// Тип крови
        /// </summary>
        /// <returns></returns>
        [HttpGet("blood_types")]
        [ProducesResponseType(typeof(List<DictionaryDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetBloodTypes()
        {
            return Ok(new Dictionary<int, string> { { 1, "Первая" }, { 2, "Вторая" }, { 3, "Третья" }, { 4, "Четвертая" } });
        }
    }
}