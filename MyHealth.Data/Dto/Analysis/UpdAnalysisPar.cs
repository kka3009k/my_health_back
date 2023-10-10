﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHealth.Data.Dto
{
    /// <summary>
    /// Параметер обновления анализа
    /// </summary>
    public class UpdAnalysisPar : AddAnalysisPar
    {
        [Required]
        public Guid ID { get; set; }
    }
}
