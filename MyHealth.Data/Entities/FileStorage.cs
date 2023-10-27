using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyHealth.Data.Entities
{
    /// <summary>
    /// Хранилище 
    /// </summary>
    [Table("FileStorages")]
    public class FileStorage : EntityBase<FileStorage>
    {
        public string Name { get; set; }

        public string Extension { get; set; }

        #region Relationships

        public ICollection<AnalysisFile> AnalysisFiles { get; set; }

        public ICollection<SymptomFile> SymptomFiles { get; set; }

        #endregion

        private protected override void CustomConfigure(EntityTypeBuilder<FileStorage> pBuilder)
        {

        }
    }
}
