using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHealth.Data.Dto
{
    /// <summary>
    /// Данные подтверждения регистрации
    /// </summary>
    public class ConfirmRegistrationDto : UserRegistrationDto
    {
        [Required]
        public string OTP { get; set; }
    }
}
