using System;
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
        /// <summary>
        /// Телефон
        /// </summary>
        [Required]
        [StringLength(12)]
        public string Phone { get; set; }

        [StringLength(128)]
        public string? FirstName { get; set; }

        [StringLength(128)]
        public string? LastName { get; set; }

        [StringLength(256)]
        public string? Email { get; set; }

        [StringLength(128)]
        public string? PasswordHash { get; set; }

        public RoleTypes? Role { get; set; }
    }
}
