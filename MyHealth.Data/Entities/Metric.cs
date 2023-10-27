using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyHealth.Data.Entities
{
    /// <summary>
    /// Метрики 
    /// </summary>
    [Table("Metrics")]
    public class Metric : EntityBase<Metric>
    {
        public Guid UserID { get; set; }
        public User User { get; set; }

        /// <summary>
        /// Рост
        /// </summary>
        public double? Height { get; set; }

        /// <summary>
        /// Вес
        /// </summary>
        public double? Weight { get; set; }

        /// <summary>
        /// Сатурация
        /// </summary>
        public int? Saturation { get; set; }

        /// <summary>
        /// Пульс
        /// </summary>
        public int? Pulse { get; set; }

        /// <summary>
        /// Глаз. давление левое
        /// </summary>
        public int? IntraocularPressureLeft { get; set; }

        /// <summary>
        /// Глаз. давление правое
        /// </summary>
        public int? IntraocularPressureRight { get; set; }

        /// <summary>
        /// Обхват живота
        /// </summary>
        public double? AbdominalGirth { get; set; }

        /// <summary>
        /// Арт. давление выс.
        /// </summary>
        public int? ArterialPressureUpper { get; set; }

        /// <summary>
        /// Арт. давление низ.
        /// </summary>
        public int? ArterialPressureLower { get; set; }

        /// <summary>
        /// Дата заполнения
        /// </summary>
        [Column(TypeName = "date")]
        [Required]
        public DateTime DateFilling { get; set; }


        private protected override void CustomConfigure(EntityTypeBuilder<Metric> pBuilder)
        {
            pBuilder.HasOne(h => h.User).WithMany(w => w.Metrics).HasForeignKey(h => h.UserID);
        }
    }
}
