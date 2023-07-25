using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
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
    /// Данные авторизации пользователя
    /// </summary>
    [Table("UserLoginData")]
    public class UserLoginData : EntityBase<UserLoginData>
    {
        /// <summary>
        /// Телефон
        /// </summary>
        [Required]
        [StringLength(12)]
        public string Phone { get; set; }

        [StringLength(256)]
        public string? Email { get; set; }

        [StringLength(128)]
        public string? PasswordHash { get; set; }

        public int? UserID { get; set; }
        public User? UserRef { get; set; }

        public override void CustomConfigure(EntityTypeBuilder<UserLoginData> pBuilder)
        {
            pBuilder.HasOne(h => h.UserRef).WithMany().HasForeignKey(h => h.UserID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
