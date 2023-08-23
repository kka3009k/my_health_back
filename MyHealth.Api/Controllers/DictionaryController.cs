using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyHealth.Api.Service;
using MyHealth.Data;
using MyHealth.Data.Dto;
using MyHealth.Data.Entities;
using System.ComponentModel;
using System.Net;
using System.Reflection;

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
        public IActionResult GetBloodTypes() => Ok(GetEnumValues(typeof(BloodTypes)));

        /// <summary>
        /// Пол
        /// </summary>
        /// <returns></returns>
        [HttpGet("gender_types")]
        [ProducesResponseType(typeof(List<DictionaryDto>), (int)HttpStatusCode.OK)]
        public IActionResult GetGenderTypes() => Ok(GetEnumValues(typeof(GenderTypes)));

        /// <summary>
        /// Резус фактор
        /// </summary>
        /// <returns></returns>
        [HttpGet("rh_factor_types")]
        [ProducesResponseType(typeof(List<DictionaryDto>), (int)HttpStatusCode.OK)]
        public IActionResult GetRhFactorTypes() => Ok(GetEnumValues(typeof(RhFactorTypes)));

        /// <summary>
        /// Роли системы
        /// </summary>
        /// <returns></returns>
        [HttpGet("role_types")]
        [ProducesResponseType(typeof(List<DictionaryDto>), (int)HttpStatusCode.OK)]
        public IActionResult GetRoleTypes() => Ok(GetEnumValues(typeof(RoleTypes)));

        /// <summary>
        /// Типы метрик
        /// </summary>
        /// <returns></returns>
        [HttpGet("metric_types")]
        [ProducesResponseType(typeof(List<DictionaryDto>), (int)HttpStatusCode.OK)]
        public IActionResult GetMetricTypes() => Ok(GetEnumValues(typeof(MetricTypes)));

        private List<DictionaryDto> GetEnumValues(Type pType)
        {
            var dictionaries = new List<DictionaryDto>();

            foreach (var enumMemberName in Enum.GetValues(pType))
            {
                var memInfo = pType.GetMember(enumMemberName.ToString());

                var description = memInfo[0].GetCustomAttribute<DescriptionAttribute>();

                var enumMemberValue = Convert.ToInt32(enumMemberName);

                dictionaries.Add(new DictionaryDto { Key = enumMemberValue, Value = description.Description.Trim() });
            }

            return dictionaries;
        }
    }
}