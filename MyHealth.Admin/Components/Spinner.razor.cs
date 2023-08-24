using Microsoft.AspNetCore.Components;
using MyHealth.Admin.Services;

namespace MyHealth.Admin.Components
{
    public partial class Spinner
    {
        [Inject]
        private SpinnerService _spinner { get; set; }

        private bool _isShow { get; set; }

        protected override Task OnInitializedAsync()
        {
            _spinner.OnShow += Show;
            _spinner.OnHide += Hide;
            UserStateService.OnUnauthorized += Hide;
            return base.OnInitializedAsync();
        }

        private void Show(CancellationToken? token)
        {
            InvokeAsync(() =>
            {
                _isShow = true;
                StateHasChanged();
            });
        }

        private void Hide(CancellationToken? token)
        {
            InvokeAsync(() =>
            {
                _isShow = false;
                StateHasChanged();
            });
        }
    }
}
