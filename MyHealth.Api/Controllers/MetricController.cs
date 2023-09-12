using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyHealth.Api.Extension;
using MyHealth.Api.Service;
using MyHealth.Data;
using MyHealth.Data.Dto;
using MyHealth.Data.Entities;
using System.Net;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System;

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
        private UserContextService _contextService;

        public MetricController(ILogger<MetricController> logger, MyDbContext pDb, UserContextService pContextService)
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
        /// вернет данные с DateFilling = текущая дата
        /// </remarks>
        /// <returns></returns>
        [HttpGet("current")]
        [ProducesResponseType(typeof(MetricDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetCurrentMetric()
        {
            var currentMetric = await GetCurrentMetric(DateTime.UtcNow.Date);
            return Ok(currentMetric);
        }

        private async Task<MetricDto> GetCurrentMetric(DateTime pDateFilling)
        {
            var userId = _contextService.UserId();
            var baseQueryMetric = _db.Metrics.OrderByDescending(o => o.DateFilling).Where(w => w.UserID == userId && w.DateFilling <= pDateFilling);
            var query = from user in _db.Users
                        let Saturation = baseQueryMetric.FirstOrDefault(f => f.Saturation != null)
                        let AbdominalGirth = baseQueryMetric.FirstOrDefault(f => f.AbdominalGirth != null)
                        let ArterialPressureLower = baseQueryMetric.FirstOrDefault(f => f.ArterialPressureLower != null)
                        let ArterialPressureUpper = baseQueryMetric.FirstOrDefault(f => f.ArterialPressureUpper != null)
                        let Height = baseQueryMetric.FirstOrDefault(f => f.Height != null)
                        let IntraocularPressureLeft = baseQueryMetric.FirstOrDefault(f => f.IntraocularPressureLeft != null)
                        let IntraocularPressureRight = baseQueryMetric.FirstOrDefault(f => f.IntraocularPressureRight != null)
                        let Pulse = baseQueryMetric.FirstOrDefault(f => f.Pulse != null)
                        let Weight = baseQueryMetric.FirstOrDefault(f => f.Weight != null)
                        select new MetricDto
                        {
                            DateFilling = pDateFilling,
                            Saturation = Saturation != null ? Saturation.Saturation : null,
                            AbdominalGirth = AbdominalGirth != null ? AbdominalGirth.AbdominalGirth : null,
                            ArterialPressureLower = ArterialPressureLower != null ? ArterialPressureLower.ArterialPressureLower : null,
                            ArterialPressureUpper = ArterialPressureUpper != null ? ArterialPressureUpper.ArterialPressureUpper : null,
                            Height = Height != null ? Height.Height : null,
                            IntraocularPressureLeft = IntraocularPressureLeft != null ? IntraocularPressureLeft.IntraocularPressureLeft : null,
                            IntraocularPressureRight = IntraocularPressureRight != null ? IntraocularPressureRight.IntraocularPressureRight : null,
                            Pulse = Pulse != null ? Pulse.Pulse : null,
                            Weight = Weight != null ? Weight.Weight : null,
                        };



            var currentMetric = await query.FirstOrDefaultAsync();

            if (currentMetric == null)
                currentMetric = new MetricDto { DateFilling = pDateFilling };

            return currentMetric;
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
            metric.FillField(metric.IntraocularPressureLeft, pMetric.IntraocularPressureLeft, v => metric.IntraocularPressureLeft = v);
            metric.FillField(metric.IntraocularPressureRight, pMetric.IntraocularPressureRight, v => metric.IntraocularPressureRight = v);
            metric.FillField(metric.Pulse, pMetric.Pulse, v => metric.Pulse = v);
            metric.FillField(metric.Weight, pMetric.Weight, v => metric.Weight = v);

            await _db.SaveChangesAsync();

            var currentMetric = await GetCurrentMetric(DateTime.UtcNow.Date);
            return Ok(currentMetric);
        }

        /// <summary>
        /// Получить историю метрики пользователя
        /// </summary>
        /// <param name="pMetricType">Тип метрики</param>
        /// <param name="pStart">Дата начала</param>
        /// <param name="pEnd">Дата окончани</param>
        /// <returns></returns>
        [HttpGet("history")]
        [ProducesResponseType(typeof(List<dynamic>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetHistoryMetric(MetricTypes pMetricType, DateTime pStart, DateTime pEnd)
        {
            var userId = _contextService.UserId();

            var query = _db.Metrics
                .OrderBy(o => o.DateFilling)
                .Where(w => w.UserID == userId && w.DateFilling >= pStart && w.DateFilling <= pEnd)
                .Select("new { DateFilling, " + pMetricType switch
                {
                    MetricTypes.Height => nameof(Metric.Height),
                    MetricTypes.Weight => nameof(Metric.Weight),
                    MetricTypes.Saturation => nameof(Metric.Saturation),
                    MetricTypes.Pulse => nameof(Metric.Pulse),
                    MetricTypes.IntraocularPressure => $"{nameof(Metric.IntraocularPressureLeft)}, {nameof(Metric.IntraocularPressureRight)}",
                    MetricTypes.AbdominalGirth => nameof(Metric.AbdominalGirth),
                    MetricTypes.ArterialPressure => $"{nameof(Metric.ArterialPressureLower)}, {nameof(Metric.ArterialPressureUpper)}",
                    _ => $"{nameof(Metric.Height)}, {nameof(Metric.Weight)}, {nameof(Metric.Saturation)}, {nameof(Metric.Pulse)}, {nameof(Metric.IntraocularPressureLeft)}, {nameof(Metric.IntraocularPressureRight)}, {nameof(Metric.AbdominalGirth)}, {nameof(Metric.ArterialPressureLower)}, {nameof(Metric.ArterialPressureUpper)}"
                } + " }");

            var metrics = await query.ToDynamicListAsync();
            return Ok(metrics);
        }
    }
}