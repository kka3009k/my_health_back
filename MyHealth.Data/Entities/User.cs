using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHealth.Data.Entities
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class User : EntityBase<User>
    {
        /// <summary>
        /// Телефон
        /// </summary>
        [Required]
        [StringLength(12)]
        public string Phone { get; set; }

        [StringLength(256)]
        public string Email { get; set; }

        [StringLength(128)]
        public string PasswordHash { get; set; }

        [StringLength(128)]
        public string FirstName { get; set; }

        [StringLength(128)]
        public string LastName { get; set; }
    }
}
