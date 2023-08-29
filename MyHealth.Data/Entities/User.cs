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
    /// Пользователь
    /// </summary>
    [Table("Users")]
    public class User : EntityBase<User>
    {
        [StringLength(14)]
        public string? Phone { get; set; }

        [StringLength(128)]
        public string? FirstName { get; set; }

        [StringLength(128)]
        public string? LastName { get; set; }

        [StringLength(128)]
        public string? Patronymic { get; set; }

        [StringLength(256)]
        public string? Email { get; set; }

        [StringLength(128)]
        public string? PasswordHash { get; set; }

        public RoleTypes? Role { get; set; }

        [StringLength(128)]
        public string? FirebaseUid { get; set; }

        [NotMapped]
        public string UserName => !string.IsNullOrWhiteSpace(Phone) ? Phone : Email;

        [StringLength(14)]
        public string? INN { get; set; }

        [Column(TypeName = "date")]
        public DateTime? BirthDate { get; set; }

        public string? Address { get; set; }

        public GenderTypes? Gender { get; set; }

        public BloodTypes? Blood { get; set; }

        public RhFactorTypes? RhFactor { get; set; }

        public int? AvatarFileID { get; set; }
        public FileStorage? AvatarFile { get; set; }

        [NotMapped]
        public string FullName => $"{LastName} {FirstName} {Patronymic}".Trim();

        #region Relationships

        public ICollection<Metric> Metrics { get; set; }

        #endregion

        private protected override void CustomConfigure(EntityTypeBuilder<User> pBuilder)
        {
            pBuilder.HasOne(h => h.AvatarFile).WithMany().HasForeignKey(h => h.AvatarFileID);
        }
    }
}
