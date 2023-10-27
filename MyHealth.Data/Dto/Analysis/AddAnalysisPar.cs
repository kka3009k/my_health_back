using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

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
        public Guid? LaboratoryID { get; set; }

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
