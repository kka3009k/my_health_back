using MyHealth.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHealth.Data.Dto
{
    /// <summary>
    /// Данные пользователя
    /// </summary>
    public class UserDto
    {
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

        /// <summary>
        /// ИНН
        /// </summary>
        [StringLength(14)]
        public string? INN { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// Адрес
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Пол
        /// </summary>
        public GenderTypes? Gender { get; set; }

        /// <summary>
        /// Тип крови
        /// </summary>
        public BloodTypes? Blood { get; set; }

        /// <summary>
        /// Резус фактор
        /// </summary>
        public RhFactorTypes? RhFactor { get; set; }

        /// <summary>
        /// Путь к фото профиля
        /// </summary>
        public string? AvatarUrl { get; set; }

        /// <summary>
        /// Главный пользователь
        /// </summary>
        public bool? IsMain { get; set; }
    }
}
