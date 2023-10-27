using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHealth.Data.Dto
{
    /// <summary>
    /// Подтверждение регистрации через почту
    /// </summary>
    public class ConfirmRegistrationPar : RegistrationPar
    {
        /// <summary>
        /// Код подтверждения
        /// </summary>
        [Required]
        public string Otp { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}
