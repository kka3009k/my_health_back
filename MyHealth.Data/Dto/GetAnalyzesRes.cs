using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHealth.Data.Dto
{
    /// <summary>
    /// Анализ
    /// </summary>
    public class GetAnalyzesRes : EntityBaseDto
    {
        /// <summary>
        /// Наименование анализа
        /// </summary>
        public string AnalysisTypeName { get; set; }

        /// <summary>
        /// Дата анализа
        /// </summary>
        public DateTime Date { get; set; }
    }
}
