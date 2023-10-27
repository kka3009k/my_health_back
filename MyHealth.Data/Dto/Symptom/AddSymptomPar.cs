using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MyHealth.Data.Dto
{
    /// <summary>
    /// Параметер добавления симптома
    /// </summary>
    public class AddSymptomPar
    {
        /// <summary>
        /// Описание
        /// </summary>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Дата
        /// </summary>
        [Required]
        public DateTime Date { get; set; }

        public List<IFormFile>? Files { get; set; }
    }
}
