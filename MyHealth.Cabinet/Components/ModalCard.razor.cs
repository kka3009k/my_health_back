using Blazored.Modal;
using Microsoft.AspNetCore.Components;

namespace MyHealth.Cabinet.Components
{
    public partial class ModalCard
    {
        [CascadingParameter]
        public BlazoredModalInstance BlazoredModal { get; set; } = default!;

        [Parameter]
        public string Message { get; set; }

        [Parameter]
        public EventCallback OkClick { get; set; }

        [Parameter]
        public EventCallback CancelClick { get; set; }

        private async Task OnOkClick()
        {
            await OkClick.InvokeAsync();
            await Close();
        }

        private async Task OnCancelClick()
        {
            await CancelClick.InvokeAsync();
            await Close();
        }

        private async Task Close() => await BlazoredModal.CloseAsync();
    }
}
