using System.ComponentModel.DataAnnotations;

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
