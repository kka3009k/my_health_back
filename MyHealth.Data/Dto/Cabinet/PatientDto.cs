using MyHealth.Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyHealth.Data.Dto.Cabinet
{
    /// <summary>
    /// Данные пациента
    /// </summary>
    public class PatientDto
    {
        public UserDto? User { get; set; }

        /// <summary>
        /// Мать
        /// </summary>
        public string? MothersName { get; set; }

        /// <summary>
        /// Отец
        /// </summary>
        public string? FathersName { get; set; }

        /// <summary>
        /// Является ребенком
        /// </summary>
        public bool IsChild => !string.IsNullOrWhiteSpace(MothersName) || !string.IsNullOrWhiteSpace(FathersName);

        /// <summary>
        /// Адрес
        /// </summary>
        public AddressDto? Address { get; set; }
    }
}
