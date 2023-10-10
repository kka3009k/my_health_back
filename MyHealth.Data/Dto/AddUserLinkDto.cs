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
    /// Данные добавдения доп. профиля
    /// </summary>
    public class AddUserLinkDto
    {
        /// <summary>
        /// Тип связи
        /// </summary>
        [Required]
        public Guid UserLinkTypeID { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        [StringLength(128)]
        [Required]
        public string FirstName { get; set; }

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
    }
}
