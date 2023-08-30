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
    /// Симптом
    /// </summary>
    public class SymptomDto : UpdSymptomPar
    {
        public new List<FileDto> Files { get; set; }
    }
}
