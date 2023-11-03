using System.ComponentModel.DataAnnotations;

namespace MyHealth.Data.Dto
{
    /// <summary>
    /// Адрес
    /// </summary>
    public class AddressDto
    {
        /// <summary>
        /// Код адреса
        /// </summary>
        public Guid? AddressID { get; set; }

        /// <summary>
        /// Область
        /// </summary>
        [StringLength(28)]
        public string? Region { get; set; }

        /// <summary>
        /// Город
        /// </summary>
        [StringLength(128)]
        public string? City { get; set; }

        /// <summary>
        /// Улица
        /// </summary>
        [StringLength(128)]
        public string? Street { get; set; }

        /// <summary>
        /// Дом
        /// </summary>
        [StringLength(128)]
        public string? House { get; set; }
    }
}
