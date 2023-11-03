using Microsoft.AspNetCore.Components;
using MyHealth.Cabinet.Services;

namespace MyHealth.Cabinet.Shared
{
    public partial class MainLayout
    {
        [Inject]
        private UserStateService _userState { get; set; }

        [Inject]
        private NavigationService _navigationService { get; set; }

        private bool _initialized;

        protected override async Task OnInitializedAsync()
        {
            await InitUserState();
        }

        private async Task InitUserState()
        {
            _userState.AuthData = await _userState.GetAuthData();
            UserStateService.OnUnauthorized += OnUnauthorized;
            _userState.OnSignOut += CheckAuth;
            _userState.OnSignIn += CheckAuth;
            _initialized = true;
            CheckAuth(CancellationToken.None);
        }

        private void CheckAuth(CancellationToken? token)
        {
            _navigationService.NavigateTo(_userState.IsAuthenticated ? "/" : "auth");
        }

        private async void OnUnauthorized(CancellationToken? token)
        {
            await _userState.SignOut(token);
        }
    }
}
