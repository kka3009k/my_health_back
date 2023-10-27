using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyHealth.Data.Entities
{
    /// <summary>
    /// Анализ 
    /// </summary>
    [Table("Analyzes")]
    public class Analysis : EntityBase<Analysis>
    {
        public Guid UserID { get; set; }
        public User User { get; set; }


        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        /// <summary>
        /// Дата анализа
        /// </summary>
        [Column(TypeName = "date")]
        [Required]
        public DateTime Date { get; set; }

        public Guid? LaboratoryID { get; set; }
        public Laboratory? Laboratory { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        public double? Price { get; set; }

        /// <summary>
        /// Доп. информация
        /// </summary>
        [StringLength(150)]
        public string? ExtraInfo { get; set; }


        private protected override void CustomConfigure(EntityTypeBuilder<Analysis> pBuilder)
        {
            pBuilder.HasOne(h => h.Laboratory).WithMany().HasForeignKey(h => h.LaboratoryID);
            pBuilder.HasOne(h => h.User).WithMany().HasForeignKey(h => h.UserID);
        }
    }
}
