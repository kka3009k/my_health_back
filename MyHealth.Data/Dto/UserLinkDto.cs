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
    /// Связи пользователя
    /// </summary>
    public class UserLinkDto
    {
        /// <summary>
        /// Второстипенный пользвотель
        /// </summary>
        [Required]
        public int SecondaryUserID { get; set; }

        /// <summary>
        /// ФИО
        /// </summary>
        public string? FullName { get; set; }

        /// <summary>
        /// Тип связи
        /// </summary>
        [Required]
        public int UserLinkTypeID { get; set; }

        /// <summary>
        /// Путь к фото профиля
        /// </summary>
        public string? AvatarUrl { get; set; }
    }
}
