using Microsoft.AspNetCore.Components;
using MyHealth.Common;
using MyHealth.Data.Dto;
using System.ComponentModel;
using System.Reflection;

namespace MyHealth.Admin.Components
{
    public partial class EnumField<TEnum>
    {
        [Parameter]
        public TEnum Value { get; set; }

        [Parameter]
        public EventCallback<TEnum> ValueChanged { get; set; }

        private List<DictionaryDto<TEnum, string>> _values;

        protected override void OnInitialized()
        {
            _values = GetEnumValues(ValueUtil.GetType(typeof(TEnum)));
        }

        private List<DictionaryDto<TEnum, string>> GetEnumValues(Type pType)
        {
            var dictionaries = new List<DictionaryDto<TEnum, string>>();

            foreach (var enumMemberName in Enum.GetValues(pType))
            {
                var memInfo = pType.GetMember(enumMemberName.ToString());

                var description = memInfo[0].GetCustomAttribute<DescriptionAttribute>();

                dictionaries.Add(new DictionaryDto<TEnum, string> { Key = (TEnum)enumMemberName, Value = description.Description.Trim() });
            }

            return dictionaries;
        }
    }
}
