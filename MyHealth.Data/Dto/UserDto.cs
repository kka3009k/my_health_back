using MyHealth.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHealth.Data.Dto
{
    public class UserDto
    {
        public int? UserID { get; set; }

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

        public RoleTypes? Role { get; set; }
    }
}
