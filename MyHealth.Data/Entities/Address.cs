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
    /// Адрес
    /// </summary>
    [Table("Address")]
    public class Address : EntityBase<Address>
    {
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

        

        #region Relationships


        #endregion
        
    }
}
