using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHealth.Data.Dto
{
    /// <summary>
    /// Анализ
    /// </summary>
    public class AnalysisDto : UpdAnalysisPar
    {
        /// <summary>
        /// Путь к файлу анализа
        /// </summary>
        public new string? File { get; set; }
    }
}
