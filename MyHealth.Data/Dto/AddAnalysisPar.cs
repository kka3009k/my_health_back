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
    /// Параметер добавления анализа
    /// </summary>
    public class AddAnalysisPar
    {
        /// <summary>
        /// Тип анализа
        /// </summary>
        [Required]
        public int AnalysisTypeID { get; set; }

        /// <summary>
        /// Дата анализа
        /// </summary>
        [Required]
        public DateTime Date { get; set; }

        /// <summary>
        /// Лаборатория
        /// </summary>
        public int? LaboratoryID { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        public double? Price { get; set; }

        /// <summary>
        /// Доп. информация
        /// </summary>
        public string? ExtraInfo { get; set; }

        public IFormFile? File { get; set; }
    }
}
