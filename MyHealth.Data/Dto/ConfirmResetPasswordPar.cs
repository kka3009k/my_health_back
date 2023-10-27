using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHealth.Data.Dto
{
    /// <summary>
    /// Данные подтверждения сброса пароля
    /// </summary>
    public class ConfirmResetPasswordPar : RegistrationPar
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
        public string NewPassword { get; set; }
    }
}
