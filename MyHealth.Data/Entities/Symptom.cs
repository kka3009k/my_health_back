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
    /// Симптом 
    /// </summary>
    [Table("Symptoms")]
    public class Symptom : EntityBase<Symptom>
    {
        public int UserID { get; set; }
        public User User { get; set; }


        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Дата
        /// </summary>
        [Column(TypeName = "date")]
        [Required]
        public DateTime Date { get; set; }

        private protected override void CustomConfigure(EntityTypeBuilder<Symptom> pBuilder)
        {
           
        }
    }
}
