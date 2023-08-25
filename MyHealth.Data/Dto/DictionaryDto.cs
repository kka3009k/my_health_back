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
    public class DictionaryDto<TKey, TValue>
    {
        /// <summary>
        /// Ключ
        /// </summary>
        public TKey Key { get; set; }

        /// <summary>
        /// Значение
        /// </summary>
        public TValue Value { get; set; }
    }


    public class DictionaryDto : DictionaryDto<int?, string?>
    {

    }
}
