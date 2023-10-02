using Microsoft.EntityFrameworkCore.Metadata.Builders;
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
    /// Связи пользователя
    /// </summary>
    [Table("UserLinks")]
    public class UserLink : EntityBase<UserLink>
    {
        [Required]
        public int MainUserID { get; set; }
        public User MainUser { get; set; }

        [Required]
        public int SecondaryUserID { get; set; }
        public User SecondaryUser { get; set; }

        [Required]
        public int UserLinkTypeID { get; set; }
        public UserLinkType UserLinkType { get; set; }

        private protected override void CustomConfigure(EntityTypeBuilder<UserLink> pBuilder)
        {
            pBuilder.HasOne(h => h.MainUser).WithMany().HasForeignKey(h => h.MainUserID);
            pBuilder.HasOne(h => h.SecondaryUser).WithMany().HasForeignKey(h => h.SecondaryUserID);
            pBuilder.HasOne(h => h.UserLinkType).WithMany().HasForeignKey(h => h.UserLinkTypeID);
        }
    }
}
