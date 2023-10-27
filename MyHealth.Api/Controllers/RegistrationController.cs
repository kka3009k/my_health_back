using Microsoft.AspNetCore.Mvc;
using MyHealth.Api.Service;
using MyHealth.Data.Dto;
using System.Net;


namespace MyHealth.Api.Controllers
{
    /// <summary>
    /// Регистрация
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class RegistrationController : ControllerBase
    {
        private readonly ILogger<RegistrationController> _logger;
        private readonly RegistrationService _registrationService;

        public RegistrationController(ILogger<RegistrationController> logger, RegistrationService pRegistrationService)
        {
            _logger = logger;
            _registrationService = pRegistrationService;
        }

        /// <summary>
        /// Регистрация через
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationPar pRegistrationPar)
        {
            try
            {
                await _registrationService.RegistrationAsync(pRegistrationPar);
                return Ok("На вашу почту был отправлен проверочный код");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Подтверждение регистрации через
        /// </summary>
        /// <returns></returns>
        [HttpPost("confirm")]
        [ProducesResponseType(typeof(AuthResDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ConfirmRegistration(ConfirmRegistrationPar pRegistrationPar)
        {
            try
            {
                var res = await _registrationService.ConfirmRegistrationAsync(pRegistrationPar);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest(ex.Message);
            }
        }
    }
}