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

        public List<IFormFile> Files { get; set; }
    }
}
