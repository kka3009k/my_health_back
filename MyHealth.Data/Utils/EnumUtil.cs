using MyHealth.Data.Dto;
using System.ComponentModel;
using System.Reflection;

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
