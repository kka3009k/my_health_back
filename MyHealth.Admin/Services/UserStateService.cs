using Blazored.LocalStorage;

namespace MyHealth.Admin.Services
{
    public class UserStateService
    {
        public static Action<CancellationToken?> OnUnauthorized { get; set; }

        public Action<CancellationToken?> OnSignOut { get; set; }
        public Action<CancellationToken?> OnSignIn { get; set; }

        public bool IsAuthenticated => !string.IsNullOrWhiteSpace(Token);

        private ILocalStorageService _localStorageService;

        private string Token { get; set; }

        public UserStateService(ILocalStorageService pLocalStorageService)
        {
            _localStorageService = pLocalStorageService;
        }

        public async Task InitUserStateAsync()
        {
            Token = await GetTokenAsync(default);
        }

        public async Task<string> GetTokenAsync(CancellationToken pCancellationToken)
        {
            var token = await _localStorageService.GetItemAsStringAsync("token");
            return token;
        }

        public async Task SignInAsync(string pToken, CancellationToken pCancellationToken = default)
        {
            await _localStorageService.SetItemAsync("token", pToken);
            Token = pToken;
            OnSignIn?.Invoke(pCancellationToken);
        }

        public async Task SignOutAsync(CancellationToken token = default)
        {
            Token = string.Empty;
            await _localStorageService.RemoveItemAsync("Token", token);
            OnSignOut?.Invoke(token);
        }
    }
}
