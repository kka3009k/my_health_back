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
    /// Тип анализа
    /// </summary>
    [Table("AnalysisTypes")]
    public class AnalysisType : EntityBase<AnalysisType>
    {
        /// <summary>
        /// Наименование
        /// </summary>
        [StringLength(1000)]
        public string Name { get; set; }
    }
}
