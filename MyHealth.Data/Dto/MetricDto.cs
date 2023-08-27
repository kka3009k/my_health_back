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
    /// Метрика
    /// </summary>
    public class MetricDto
    {
        /// <summary>
        /// Рост
        /// </summary>
        public double? Height { get; set; }

        /// <summary>
        /// Вес
        /// </summary>
        public double? Weight { get; set; }

        /// <summary>
        /// Сатурация
        /// </summary>
        public int? Saturation { get; set; }

        /// <summary>
        /// Пульс
        /// </summary>
        public int? Pulse { get; set; }

        /// <summary>
        /// Глаз. давление левое
        /// </summary>
        [Range(0, 35)]
        public int? IntraocularPressureLeft { get; set; }

        /// <summary>
        /// Глаз. давление правое
        /// </summary>
        [Range(0, 35)]
        public int? IntraocularPressureRight { get; set; }

        /// <summary>
        /// Обхват живота
        /// </summary>
        public double? AbdominalGirth { get; set; }

        /// <summary>
        /// Арт. давление выс.
        /// </summary>
        public int? ArterialPressureUpper { get; set; }

        /// <summary>
        /// Арт. давление низ.
        /// </summary>
        public int? ArterialPressureLower { get; set; }

        /// <summary>
        /// Дата заполнения
        /// </summary>
        [Required]
        public DateTime DateFilling { get; set; }
    }
}
