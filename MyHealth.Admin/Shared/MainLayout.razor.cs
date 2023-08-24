using Microsoft.AspNetCore.Components;
using MyHealth.Admin.Services;

namespace MyHealth.Admin.Shared
{
    public partial class MainLayout
    {
        [Inject]
        private UserStateService _userStateService { get; set; }

        [Inject]
        private NavigationManager _navigationManager { get; set; }

        private bool _initialized;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                UserStateService.OnUnauthorized += OnUnauthorized;
                await _userStateService.InitUserStateAsync();
                _userStateService.OnSignOut += CheckAuth;
                _userStateService.OnSignIn += CheckAuth;
                _initialized = true;
                CheckAuth(CancellationToken.None);
            }
        }

        private void CheckAuth(CancellationToken? token)
        {
            _navigationManager.NavigateTo(_userStateService.IsAuthenticated ? "/" : "/login");
        }

        private async void OnUnauthorized(CancellationToken? token)
        {
            await _userStateService.SignOutAsync(token ?? default);
        }
    }
}
