using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHealth.Data.Dto
{
    /// <summary>
    /// Параметер обновления симптома
    /// </summary>
    public class UpdSymptomPar : AddSymptomPar
    {
        [Required]
        public Guid ID { get; set; }
    }
}
