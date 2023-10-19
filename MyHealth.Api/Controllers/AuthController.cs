using DotNetEnv;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyHealth.Api.Service;
using MyHealth.Api.Utils;
using MyHealth.Data;
using MyHealth.Data.Dto;
using MyHealth.Data.Entities;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Dynamic.Core.Tokenizer;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using Firebase_Auth = FirebaseAdmin.Auth.FirebaseAuth;

namespace MyHealth.Api.Controllers
{
    /// <summary>
    /// Авторизация
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly AuthService _authService;

        public AuthController(ILogger<AuthController> logger, AuthService pAuthService)
        {
            _logger = logger;
            _authService = pAuthService;
        }

        /// <summary>
        /// Авторизация через Firebase
        /// </summary>
        /// <param name="token">Токен firebase</param>
        /// <returns></returns>
        [HttpPost("firebase")]
        [ProducesResponseType(typeof(AuthResDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> FirebaseAuth(string token)
        {
            try
            {
                var res = await _authService.FirebaseAuthAsync(token);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Авторизация через Firebase по Uid, использовать только для тестов
        /// </summary>
        /// <param name="uid">Индентификатор firebase</param>
        /// <returns></returns>
        [HttpPost("firebase/uid")]
        [ProducesResponseType(typeof(AuthResDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> FirebaseAuthUid(string uid)
        {
            try
            {
                var res = await _authService.FirebaseAuthUidAsync(uid);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Авторизация через Логин
        /// </summary>
        /// <param name="pAuthPar">Данные авторизации</param>
        /// <returns></returns>
        [HttpPost("login")]
        [ProducesResponseType(typeof(AuthResDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> LoginAuth(AuthPar pAuthPar)
        {
            try
            {
                var res = await _authService.LoginAuthAsync(pAuthPar);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Обновление токена
        /// </summary>
        /// <param name="refreshToken">Токен обновления</param>
        /// <returns></returns>
        [HttpPost("refresh")]
        [ProducesResponseType(typeof(AuthResDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> RefreshToken(string refreshToken)
        {
            try
            {
                var res = await _authService.RefreshTokenAsync(refreshToken);
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