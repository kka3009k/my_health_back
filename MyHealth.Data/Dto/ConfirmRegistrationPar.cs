using System;
using System.Collections.Generic;
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
        /// Код подтверждения регистрации
        /// </summary>
        public string Otp { get; set; }
    }
}
