using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHealth.Data.Dto
{
    /// <summary>
    /// Справочник
    /// </summary>
    public class DictionaryDto
    {
        /// <summary>
        /// Ключ
        /// </summary>
        public int? Key { get; set; }

        /// <summary>
        /// Значение
        /// </summary>
        public string? Value { get; set; }
    }
}
