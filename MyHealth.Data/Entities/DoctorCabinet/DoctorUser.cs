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
    /// Доктор
    /// </summary>
    [Table("DoctorUser")]
    public class DoctorUser : EntityBase<DoctorUser>
    {
        /// <summary>
        /// Доктор
        /// </summary>
        public Guid UserID { get; set; }
        public User User { get; set; }

        /// <summary>
        /// Специальность
        /// </summary>
        public string Speciality { get; set; }

        /// <summary>
        /// Дипломы/Сертификаты
        /// </summary>
        public string Diplomas { get; set; }

        /// <summary>
        /// Адрес
        /// </summary>
        public Guid AddressID { get; set; }
        public Address Address { get; set; }


        #region Relationships


        #endregion

        private protected override void CustomConfigure(EntityTypeBuilder<DoctorUser> pBuilder)
        {
            pBuilder.HasOne(h => h.User).WithMany().HasForeignKey(h => h.UserID);
            pBuilder.HasOne(h => h.Address).WithMany().HasForeignKey(h => h.AddressID);
        }
    }
}
