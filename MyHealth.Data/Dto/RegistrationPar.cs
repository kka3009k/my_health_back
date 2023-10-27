using System.ComponentModel.DataAnnotations;

namespace MyHealth.Data.Dto
{
    /// <summary>
    /// Данные регистрации через почту
    /// </summary>
    public class RegistrationPar
    {
        /// <summary>
        /// Почта
        /// </summary>
        [Required]
        public string Email { get; set; }
    }
}
