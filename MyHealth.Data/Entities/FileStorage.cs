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
