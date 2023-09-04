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
        /// Наименование
        /// </summary>
        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        /// <summary>
        /// Дата анализа
        /// </summary>
        [Required]
        public DateTime Date { get; set; }

        /// <summary>
        /// Лаборатория
        /// </summary>
        [Required]
        public int? LaboratoryID { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        public double? Price { get; set; }

        /// <summary>
        /// Доп. информация
        /// </summary>
        [StringLength(150)]
        public string? ExtraInfo { get; set; }

        public IFormFile? File { get; set; }
    }
}
