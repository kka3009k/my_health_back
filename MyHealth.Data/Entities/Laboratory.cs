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
    /// Лаборатория
    /// </summary>
    [Table("Laboratories")]
    public class Laboratory : EntityBase<Laboratory>
    {
        /// <summary>
        /// Наименование
        /// </summary>
        [StringLength(350)]
        public string Name { get; set; }
    }
}
