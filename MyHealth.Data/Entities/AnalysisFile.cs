using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyHealth.Data.Entities
{
    /// <summary>
    /// Файл анализа
    /// </summary>
    [Table("AnalysisFiles")]
    public class AnalysisFile : EntityBase<AnalysisFile>
    {
        public Guid AnalysisID { get; set; }
        public Analysis Analysis { get; set; }

        public Guid FileID { get; set; }
        public FileStorage File { get; set; }

        private protected override void CustomConfigure(EntityTypeBuilder<AnalysisFile> pBuilder)
        {
            pBuilder.HasOne(h => h.Analysis).WithMany().HasForeignKey(h => h.AnalysisID);
            pBuilder.HasOne(h => h.File).WithMany(w => w.AnalysisFiles).HasForeignKey(h => h.FileID);
        }
    }
}
