using Blazored.LocalStorage;

namespace MyHealth.Admin.Services
{
    public class UserStateService
    {
        public static Action<CancellationToken?> OnUnauthorized { get; set; }

        public Action<CancellationToken?> OnSignOut { get; set; }
        public Action<CancellationToken?> OnSignIn { get; set; }

        public bool IsAuthenticated => !string.IsNullOrWhiteSpace(token);

        private ILocalStorageService _localStorageService;

        private string token { get; set; }



        public UserStateService(ILocalStorageService pLocalStorageService)
        {
            _localStorageService = pLocalStorageService;
        }

        public async Task InitUserStateAsync()
        {
            token = await GetTokenAsync(default);
        }

        public async Task<string> GetTokenAsync(CancellationToken pCancellationToken)
        {
            var token = await _localStorageService.GetItemAsStringAsync("token");
            return token;
        }

        public async Task SignInAsync(string pToken, CancellationToken pCancellationToken = default)
        {
            await _localStorageService.SetItemAsync("token", pToken);
            token = pToken;
            OnSignIn?.Invoke(pCancellationToken);
        }

        public async Task SignOutAsync(CancellationToken pCancellationToken = default)
        {
            token = string.Empty;
            await _localStorageService.RemoveItemAsync("token", pCancellationToken);
            OnSignOut?.Invoke(pCancellationToken);
        }
    }
}
