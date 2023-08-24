using Microsoft.AspNetCore.Components;
using MyHealth.Admin.Services;

namespace MyHealth.Admin.Components
{
    public partial class Header
    {
        [Parameter]
        public EventCallback<Type> Click { get; set; }

        [Inject]
        private UserStateService _userStateService { get; set; }

        private async Task OnSignOutClick()
        {
            await _userStateService.SignOutAsync();
        }
    }
}
