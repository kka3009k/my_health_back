using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
