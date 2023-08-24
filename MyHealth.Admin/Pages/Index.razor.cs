using Microsoft.AspNetCore.Components;
using MyHealth.Admin.Forms;
using Radzen;

namespace MyHealth.Admin.Pages
{
    public partial class Index
    {
        private RenderFragment _form;

        private void OnMenuItemClick(Type pFormType)
        {
            _form = builder =>
            {

                builder.OpenComponent(0, pFormType);
                builder.CloseComponent();
            };
        }
    }
}
