using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHealth.Data.Dto
{
    /// <summary>
    /// Данные изменения пароля
    /// </summary>
    public class ChangePasswordPar
    {
        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Новый пароль
        /// </summary>
        public string NewPassword { get; set; }
    }
}
