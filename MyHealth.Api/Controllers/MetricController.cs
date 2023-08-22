using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyHealth.Api.Extension;
using MyHealth.Api.Service;
using MyHealth.Data;
using MyHealth.Data.Dto;
using MyHealth.Data.Entities;
using System.Net;

namespace MyHealth.Api.Controllers
{
    /// <summary>
    /// Работа с метриками пользователя
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class MetricController : ControllerBase
    {
        private readonly ILogger<MetricController> _logger;
        private readonly MyDbContext _db;
        private HttpContextService _contextService;

        public MetricController(ILogger<MetricController> logger, MyDbContext pDb, HttpContextService pContextService)
        {
            _logger = logger;
            _db = pDb;
            _contextService = pContextService;
        }

        /// <summary>
        /// Получить текущие метрики пользователя.
        /// </summary>
        /// <remarks>
        /// Возравщает актуальную информацию по метрики отсортированную по DateFilling,
        /// если метрики не найдены, то вернет пустые данные с DateFilling = текущая дата
        /// </remarks>
        /// <returns></returns>
        [HttpGet("current")]
        [ProducesResponseType(typeof(MetricDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetCurrentMetric()
        {
            var userId = _contextService.UserId();
            var hasUser = await _db.Users.AnyAsync(f => f.ID == userId);

            if (!hasUser)
                return BadRequest("Пользователь не найден");

            var currentMetric = await _db.Metrics.OrderByDescending(o => o.DateFilling).FirstOrDefaultAsync(f => f.UserID == userId);

            if (currentMetric == null)
                return Ok(new MetricDto { DateFilling = DateTime.UtcNow.Date, });

            return Ok(new MetricDto
            {
                Saturation = currentMetric.Saturation,
                AbdominalGirth = currentMetric.AbdominalGirth,
                ArterialPressureLower = currentMetric.ArterialPressureLower,
                ArterialPressureUpper = currentMetric.ArterialPressureUpper,
                DateFilling = currentMetric.DateFilling,
                Height = currentMetric.Height,
                IntraocularPressure = currentMetric.IntraocularPressure,
                Pulse = currentMetric.Pulse,
                Weight = currentMetric.Weight,
            });
        }

        /// <summary>
        /// Обновить данные метрики пользователя
        /// </summary>
        /// <param name="pMetric"></param>
        /// <returns></returns>
        [HttpPost("update")]
        [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateMetric([FromBody] MetricDto pMetric)
        {
            var userId = _contextService.UserId();
            var hasUser = await _db.Users.AnyAsync(f => f.ID == userId);

            if (!hasUser)
                return BadRequest("Пользователь не найден");

            var metric = await _db.Metrics.FirstOrDefaultAsync(f => f.UserID == userId && f.DateFilling == pMetric.DateFilling);

            if (metric == null)
            {
                metric = new Metric();
                metric.DateFilling = pMetric.DateFilling;
                metric.UserID = userId;
                await _db.AddAsync(metric);
            }

            metric.FillField(metric.Saturation, pMetric.Saturation, v => metric.Saturation = v);
            metric.FillField(metric.AbdominalGirth, pMetric.AbdominalGirth, v => metric.AbdominalGirth = v);
            metric.FillField(metric.ArterialPressureLower, pMetric.ArterialPressureLower, v => metric.ArterialPressureLower = v);
            metric.FillField(metric.ArterialPressureUpper, pMetric.ArterialPressureUpper, v => metric.ArterialPressureUpper = v);
            metric.FillField(metric.Height, pMetric.Height, v => metric.Height = v);
            metric.FillField(metric.IntraocularPressure, pMetric.IntraocularPressure, v => metric.IntraocularPressure = v);
            metric.FillField(metric.Pulse, pMetric.Pulse, v => metric.Pulse = v);
            metric.FillField(metric.Weight, pMetric.Weight, v => metric.Weight = v);

            await _db.SaveChangesAsync();

            return Ok(pMetric);
        }

        /// <summary>
        /// Получить историю метрики пользователя
        /// </summary>
        /// <param name="pStart">Дата начала</param>
        /// <param name="pEnd">Дата окончания</param>
        /// <returns></returns>
        [HttpGet("history")]
        [ProducesResponseType(typeof(List<MetricDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetHistoryMetric(DateTime pStart, DateTime pEnd)
        {
            var userId = _contextService.UserId();
            var hasUser = await _db.Users.AnyAsync(f => f.ID == userId);

            if (!hasUser)
                return BadRequest("Пользователь не найден");

            var metrics = await _db.Metrics.Where(w => w.UserID == userId && w.DateFilling >= pStart && w.DateFilling <= pEnd).ToListAsync();
            return Ok(metrics.Select(s => new MetricDto
            {
                Saturation = s.Saturation,
                AbdominalGirth = s.AbdominalGirth,
                ArterialPressureLower = s.ArterialPressureLower,
                ArterialPressureUpper = s.ArterialPressureUpper,
                DateFilling = s.DateFilling,
                Height = s.Height,
                IntraocularPressure = s.IntraocularPressure,
                Pulse = s.Pulse,
                Weight = s.Weight,
            }).ToList());
        }
    }
}