using Microsoft.AspNetCore.Components;
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

        private List<DictionaryDto> _values;

        protected override void OnInitialized()
        {
            _values = GetEnumValues(typeof(TEnum));
        }

        private List<DictionaryDto> GetEnumValues(Type pType)
        {
            var dictionaries = new List<DictionaryDto>();

            foreach (var enumMemberName in Enum.GetValues(pType))
            {
                var memInfo = pType.GetMember(enumMemberName.ToString());

                var description = memInfo[0].GetCustomAttribute<DescriptionAttribute>();

                var enumMemberValue = Convert.ToInt32(enumMemberName);

                dictionaries.Add(new DictionaryDto { Key = enumMemberValue, Value = description.Description.Trim() });
            }

            return dictionaries;
        }
    }
}
