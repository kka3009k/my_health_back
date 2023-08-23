using Microsoft.AspNetCore.Components;
using MyHealth.AdminPanel.Services;

namespace MyHealth.AdminPanel.Shared
{
    public partial class MainLayout
    {
        [Inject]
        private UserStateService _userStateService { get; set; }

        [Inject]
        private NavigationManager _navigationManager { get; set; }

        private bool _initialized;

        protected override async Task OnInitializedAsync()
        {
            UserStateService.OnUnauthorized += OnUnauthorized;
            await _userStateService.InitUserStateAsync();
            _userStateService.OnSignOut += CheckAuth;
            _userStateService.OnSignIn += CheckAuth;
            _initialized = true;
            CheckAuth(CancellationToken.None);
        }

        private void CheckAuth(CancellationToken? token)
        {
            _navigationManager.NavigateTo(_userStateService.IsAuthenticated ? "adminpanel" : "login");
        }

        private async void OnUnauthorized(CancellationToken? token)
        {
            await _userStateService.SignOutAsync(token ?? default);
        }
    }
}
