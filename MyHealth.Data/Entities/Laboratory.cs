using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
