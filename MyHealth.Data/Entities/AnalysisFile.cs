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
    /// Файл анализа
    /// </summary>
    [Table("AnalysisFiles")]
    public class AnalysisFile : EntityBase<AnalysisFile>
    {
        public int AnalysisID { get; set; }
        public Analysis Analysis { get; set; }

        public int FileID { get; set; }
        public FileStorage File { get; set; }

        private protected override void CustomConfigure(EntityTypeBuilder<AnalysisFile> pBuilder)
        {
            pBuilder.HasOne(h => h.Analysis).WithMany().HasForeignKey(h => h.AnalysisID);
            pBuilder.HasOne(h => h.File).WithMany().HasForeignKey(h => h.FileID);
        }
    }
}
