using MyHealth.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace MyHealth.Data.Dto.Cabinet
{
    /// <summary>
    /// Данные врача
    /// </summary>
    public class DoctorUserDto
    {
        /// <summary>
        /// Специальность
        /// </summary>
        public string? Speciality { get; set; }

        /// <summary>
        /// Дипломы/Сертификаты
        /// </summary>
        public string? Diplomas { get; set; }

        /// <summary>
        /// Адрес
        /// </summary>
        public AddressDto? Address { get; set; }
    }
}
