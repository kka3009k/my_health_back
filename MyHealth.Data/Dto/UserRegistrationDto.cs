﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHealth.Data.Dto
{
    /// <summary>
    /// Данные регистрации
    /// </summary>
    public class UserRegistrationDto
    {
        [StringLength(12)]
        [Required]
        public string Phone { get; set; }

        [StringLength(128)]
        [Required]
        public string FirstName { get; set; }

        [StringLength(128)]
        [Required]
        public string LastName { get; set; }

        [StringLength(256)]
        public string? Email { get; set; }

        [StringLength(64)]
        [Required]
        public string Password { get; set; }
    }
}
