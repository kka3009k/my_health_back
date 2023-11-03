using Blazored.LocalStorage;
using MyHealth.Data.Dto;
using Newtonsoft.Json;
using System.Data;

namespace MyHealth.Cabinet.Services
{
    public class UserStateService
    {
        public AuthResDto AuthData { get; set; }

        public static UserStateService StaticState { get; set; }

        public bool IsAuthenticated => !string.IsNullOrWhiteSpace(AuthData?.AccessToken);

        public Action<CancellationToken?> OnSignOut { get; set; }

        public Action<CancellationToken?> OnSignIn { get; set; }

        public UserDto CurrentUser { get; set; }

        private ILocalStorageService _localStorage { get; set; }

        public UserStateService(ILocalStorageService pLocalStorage)
        {
            _localStorage = pLocalStorage;
        }

        public async Task SignOut(CancellationToken? token = null)
        {
            AuthData.AccessToken = string.Empty;
            await _localStorage.RemoveItemAsync("auth_data", token ?? CancellationToken.None);
            StaticState = this;
            OnSignOut?.Invoke(token);
        }

        public async Task<AuthResDto> GetAuthData(CancellationToken? token = null)
        {
            var user = await _localStorage.GetItemAsync<string>("auth_data", token ?? CancellationToken.None);
            var deserializeUser = string.IsNullOrWhiteSpace(user) ? new AuthResDto() : JsonConvert.DeserializeObject<AuthResDto>(user);
            StaticState = this;
            StaticState.AuthData = deserializeUser;
            return deserializeUser;
        }

        public async Task SignIn(AuthResDto pAuthData, CancellationToken? token = null)
        {
            AuthData = pAuthData;
            await _localStorage.SetItemAsync("auth_data", JsonConvert.SerializeObject(pAuthData), token ?? CancellationToken.None);
            StaticState = this;
            OnSignIn?.Invoke(token);
        }

        public static Action<CancellationToken?> OnUnauthorized { get; set; }
    }
}
