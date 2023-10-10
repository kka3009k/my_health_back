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
    /// Файл симптома
    /// </summary>
    [Table("SymptomFiles")]
    public class SymptomFile : EntityBase<SymptomFile>
    {
        public Guid SymptomID { get; set; }
        public Symptom Symptom { get; set; }

        public Guid FileID { get; set; }
        public FileStorage File { get; set; }

        private protected override void CustomConfigure(EntityTypeBuilder<SymptomFile> pBuilder)
        {
            pBuilder.HasOne(h => h.Symptom).WithMany().HasForeignKey(h => h.SymptomID);
            pBuilder.HasOne(h => h.File).WithMany(w => w.SymptomFiles).HasForeignKey(h => h.FileID);
        }
    }
}
