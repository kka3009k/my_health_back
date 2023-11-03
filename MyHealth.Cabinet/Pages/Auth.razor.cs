using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;
using MyHealth.Cabinet.Common.Constants;
using MyHealth.Data.Entities;
using System.Net.Http.Json;
using MyHealth.Data.Dto;
using Blazored.Toast.Services;
using MyHealth.Cabinet.Services;

namespace MyHealth.Cabinet.Pages
{
    public partial class Auth
    {
        [Inject]
        private UserStateService _userState { get; set; }


        [Inject]
        private ILocalStorageService _localStorage { get; set; }

        [Inject]
        private HttpClient _client { get; set; }

        [Inject]
        private IToastService _toastService { get; set; }

        [Inject]
        private NavigationService _navigationService { get; set; }

        [Inject]
        private SpinnerService _spinnerService { get; set; }

        private string _username;
        private string _password;
        private const string LAST_LOGIN_KEY = "LastLogin";
        private string _pwdTypeInput = "password";

        protected override async Task OnAfterRenderAsync(bool pFirstRender)
        {
            if (pFirstRender)
            {
                _username = await _localStorage.GetItemAsStringAsync(LAST_LOGIN_KEY);
                await InvokeAsync(StateHasChanged);
            }
        }

        private async Task Login()
        {
            if (!Validate())
                return;

            await _spinnerService.RunAsync(async () =>
            {

                var res = await _client.PostAsJsonAsync("auth/login", new AuthPar
                {
                    Login = _username,
                    Password = _password
                });

                if (res.IsSuccessStatusCode)
                {
                    var user = await res.Content.ReadFromJsonAsync<AuthResDto>();
                    await RememberLogin();
                    await _userState.SignIn(user);
                }
                else
                {
                    var message = await res.Content.ReadAsStringAsync();
                    _toastService.ShowError(message);
                }
            });
        }

        private async Task OnKeyDown(KeyboardEventArgs pArgs)
        {
            if (IsEnter(pArgs.Code) && Validate()) await Login();
        }

        private bool IsEnter(string pCode)
            => new string[] { KeyboardKey.ENTER, KeyboardKey.NUMPAD_ENTER }.Contains(pCode);

        private bool Validate()
        {
            return !string.IsNullOrWhiteSpace(_username) && !string.IsNullOrWhiteSpace(_password);
        }

        private ValueTask RememberLogin()
        {
            return _localStorage.SetItemAsStringAsync(LAST_LOGIN_KEY, _username);
        }

        private void showPassword()
        {
            var type = _pwdTypeInput == "password" ? "text" : "password";
            _pwdTypeInput = type;
        }

        private void Registration()
        {
            _navigationService.NavigateTo("registration");
        }
    }
}
