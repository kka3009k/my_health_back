using Microsoft.AspNetCore.Components;
using MyHealth.Common;
using MyHealth.Data.Dto;
using MyHealth.Data.Utils;
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
            _values = EnumUtil.GetEnumValues<TEnum>();
        }

    }
}
