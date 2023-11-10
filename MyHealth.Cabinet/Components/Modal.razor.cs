using Microsoft.AspNetCore.Components;

namespace MyHealth.Cabinet.Components
{
    public partial class Modal
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public bool Visible { get; set; }
    }
}
