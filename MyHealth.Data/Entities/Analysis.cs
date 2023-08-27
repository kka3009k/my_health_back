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
    /// Анализ 
    /// </summary>
    [Table("Analyzes")]
    public class Analysis : EntityBase<Analysis>
    {
        public int UserID { get; set; }
        public User User { get; set; }

        public int AnalysisTypeID { get; set; }
        public AnalysisType AnalysisType { get; set; }

        /// <summary>
        /// Дата анализа
        /// </summary>
        [Column(TypeName = "date")]
        [Required]
        public DateTime Date { get; set; }

        public int? LaboratoryID { get; set; }
        public Laboratory Laboratory { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        public double? Price { get; set; }

        /// <summary>
        /// Доп. информация
        /// </summary>
        public string? ExtraInfo { get; set; }


        private protected override void CustomConfigure(EntityTypeBuilder<Analysis> pBuilder)
        {
            pBuilder.HasOne(h => h.AnalysisType).WithMany().HasForeignKey(h => h.AnalysisTypeID);
            pBuilder.HasOne(h => h.Laboratory).WithMany().HasForeignKey(h => h.LaboratoryID);
        }
    }
}
