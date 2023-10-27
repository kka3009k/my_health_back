using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
