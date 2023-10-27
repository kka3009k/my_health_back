using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyHealth.Api.Service;
using MyHealth.Data;
using MyHealth.Data.Dto;
using MyHealth.Data.Entities;
using MyHealth.Data.Utils;
using System.Net;

namespace MyHealth.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DictionaryController : ControllerBase
    {
        private readonly ILogger<DictionaryController> _logger;
        private readonly MyDbContext _db;
        private UserContextService _contextService;

        public DictionaryController(ILogger<DictionaryController> logger, MyDbContext pDb, UserContextService pContextService)
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
        [ProducesResponseType(typeof(List<DictionaryDto<int, string>>), (int)HttpStatusCode.OK)]
        public IActionResult GetBloodTypes() => Ok(EnumUtil.GetEnumValues<BloodTypes>());

        /// <summary>
        /// Пол
        /// </summary>
        /// <returns></returns>
        [HttpGet("gender_types")]
        [ProducesResponseType(typeof(List<DictionaryDto<int, string>>), (int)HttpStatusCode.OK)]
        public IActionResult GetGenderTypes() => Ok(EnumUtil.GetEnumValues<GenderTypes>());

        /// <summary>
        /// Резус фактор
        /// </summary>
        /// <returns></returns>
        [HttpGet("rh_factor_types")]
        [ProducesResponseType(typeof(List<DictionaryDto<int, string>>), (int)HttpStatusCode.OK)]
        public IActionResult GetRhFactorTypes() => Ok(EnumUtil.GetEnumValues<RhFactorTypes>());

        /// <summary>
        /// Роли системы
        /// </summary>
        /// <returns></returns>
        [HttpGet("role_types")]
        [ProducesResponseType(typeof(List<DictionaryDto<int, string>>), (int)HttpStatusCode.OK)]
        public IActionResult GetRoleTypes() => Ok(EnumUtil.GetEnumValues<RoleTypes>());

        /// <summary>
        /// Типы метрик
        /// </summary>
        /// <returns></returns>
        [HttpGet("metric_types")]
        [ProducesResponseType(typeof(List<DictionaryDto<int, string>>), (int)HttpStatusCode.OK)]
        public IActionResult GetMetricTypes() => Ok(EnumUtil.GetEnumValues<MetricTypes>());

        /// <summary>
        /// Лаборатории
        /// </summary>
        /// <returns></returns>
        [HttpGet("laboratories")]
        [ProducesResponseType(typeof(List<LaboratoryDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetLaboratories()
        {
            var laboratories = await _db.Laboratories
                 .Select(s => new LaboratoryDto { ID = s.ID, Name = s.Name })
                .ToListAsync();
            return Ok(laboratories);
        }

        /// <summary>
        /// Типы связи пользователя 
        /// </summary>
        /// <returns></returns>
        [HttpGet("user_link_types")]
        [ProducesResponseType(typeof(List<UserLinkTypeDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetUserLinkTypes()
        {
            var userLinkTypes = await _db.UserLinkTypes
                 .Select(s => new UserLinkTypeDto { ID = s.ID, Name = s.Name })
                .ToListAsync();
            return Ok(userLinkTypes);
        }


    }
}