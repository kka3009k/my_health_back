﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHealth.Data.Entities
{
    /// <summary>
    /// Пользователь
    /// </summary>
    [Table("Users")]
    public class User : EntityBase<User>
    {
        [StringLength(128)]
        [Required]
        public string FirstName { get; set; }

        [StringLength(128)]
        public string LastName { get; set; }

        public RoleTypes? Role { get; set; }
    }
}
