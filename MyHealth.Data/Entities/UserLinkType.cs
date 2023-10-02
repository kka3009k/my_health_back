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
    /// Тип связи пользователя 
    /// </summary>
    [Table("UserLinkTypes")]
    public class UserLinkType : EntityBase<UserLinkType>
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}
