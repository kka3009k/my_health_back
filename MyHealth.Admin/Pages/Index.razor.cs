using Microsoft.AspNetCore.Components;

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
