using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHealth.Data.Entities.DoctorCabinet
{
    /// <summary>
    /// Пациент
    /// </summary>
    [Table("Patients")]
    public class Patient : EntityBase<Patient>
    {
        /// <summary>
        /// Клиент
        /// </summary>
        public Guid UserID { get; set; }
        public User User { get; set; }

        /// <summary>
        /// Адрес
        /// </summary>
        public Guid? AddressID { get; set; }
        public Address? Address { get; set; }

        /// <summary>
        /// Мать
        /// </summary>
        public string? MothersName { get; set; }

        /// <summary>
        /// Отец
        /// </summary>
        public string? FathersName { get; set; }

        /// <summary>
        /// Является ребенком
        /// </summary>
        [NotMapped]
        public bool IsChild => !string.IsNullOrWhiteSpace(MothersName) || !string.IsNullOrWhiteSpace(FathersName);

        /// <summary>
        /// Доктор
        /// </summary>
        public Guid DoctorID { get; set; }
        public DoctorUser DoctorUser { get; set; }


        #region Relationships


        #endregion

        private protected override void CustomConfigure(EntityTypeBuilder<Patient> pBuilder)
        {
            pBuilder.HasOne(h => h.User).WithMany().HasForeignKey(h => h.UserID);
            pBuilder.HasOne(h => h.DoctorUser).WithMany().HasForeignKey(h => h.DoctorID);
        }
    }
}
