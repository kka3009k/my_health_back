using MyHealth.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace MyHealth.Data.Dto.Cabinet
{
    /// <summary>
    /// Данные пациента в списке
    /// </summary>
    public class PatientAllDto
    {
        public Guid PatientID { get; set; }
        /// <summary>
        /// Телефон
        /// </summary>
        [StringLength(14)]
        public string? Phone { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        [StringLength(128)]
        public string? FirstName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        [StringLength(128)]
        public string? LastName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        [StringLength(128)]
        public string? Patronymic { get; set; }

        [StringLength(256)]
        public string? Email { get; set; }
    }
}
