using MyHealth.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHealth.Data.Dto
{
    /// <summary>
    /// Результат авторизации
    /// </summary>
    public class AuthResDto
    {
        public int? UserID { get; set; }

        public string? AccessToken { get; set; }

        public DateTime? AccessTokenExpiresAt { get; set; }

        public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenExpiresAt { get; set; }
    }
}
