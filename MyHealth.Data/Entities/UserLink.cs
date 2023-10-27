using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyHealth.Data.Entities
{
    /// <summary>
    /// Связи пользователя
    /// </summary>
    [Table("UserLinks")]
    public class UserLink : EntityBase<UserLink>
    {
        [Required]
        public Guid MainUserID { get; set; }
        public User MainUser { get; set; }

        [Required]
        public Guid SecondaryUserID { get; set; }
        public User SecondaryUser { get; set; }

        [Required]
        public Guid UserLinkTypeID { get; set; }
        public UserLinkType UserLinkType { get; set; }

        private protected override void CustomConfigure(EntityTypeBuilder<UserLink> pBuilder)
        {
            pBuilder.HasOne(h => h.MainUser).WithMany().HasForeignKey(h => h.MainUserID);
            pBuilder.HasOne(h => h.SecondaryUser).WithMany().HasForeignKey(h => h.SecondaryUserID);
            pBuilder.HasOne(h => h.UserLinkType).WithMany().HasForeignKey(h => h.UserLinkTypeID);
        }
    }
}
