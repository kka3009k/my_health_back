using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHealth.Data.Dto
{
    /// <summary>
    /// Тип связи пользователя 
    /// </summary>
    public class UserLinkTypeDto : EntityBaseDto
    {
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }
    }
}
