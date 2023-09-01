using MyHealth.Data.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyHealth.Data.Utils
{
    public class EnumUtil
    {
        public static List<DictionaryDto<TEnum, string>> GetEnumValues<TEnum>()
        {
            var type = Nullable.GetUnderlyingType(typeof(TEnum)) ?? typeof(TEnum);
            var dictionaries = new List<DictionaryDto<TEnum, string>>();

            foreach (var enumMemberName in Enum.GetValues(type))
            {
                var memInfo = type.GetMember(enumMemberName.ToString());

                var description = memInfo[0].GetCustomAttribute<DescriptionAttribute>();

                dictionaries.Add(new DictionaryDto<TEnum, string> { Key = (TEnum)enumMemberName, Value = description.Description.Trim() });
            }

            return dictionaries;
        }
    }
}
