using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using MyHealth.Data.Dto;
using MyHealth.Data.Dto.Cabinet;
using MyHealth.Data.Entities;
using MyHealth.Cabinet.Services;
using System.Net.Http;
using System.Net.Http.Json;

namespace MyHealth.Cabinet.Pages
{
    public partial class PasswordRecovery
    {
        [Parameter]
        public string Email { get; set; }

        [Inject]
        private HttpClient _client { get; set; }

        [Inject]
        private IToastService _toastService { get; set; }

        [Inject]
        private NavigationService _navigationService { get; set; }

        [Inject]
        private SpinnerService _spinnerService { get; set; }

        private bool _confirmation;
        private string _username;
        private string _pwdTypeInput = "password";
        private string _password;
        private string _otp;

        protected override void OnInitialized()
        {
            _username = Email;
        }

        public async Task Next()
        {
            await _spinnerService.RunAsync(async () =>
            {

                var res = await _client.PostAsJsonAsync<object>($"auth/reset_password?email={_username}", null);

                if (res.IsSuccessStatusCode)
                {
                    var message = await res.Content.ReadAsStringAsync();
                    _toastService.ShowSuccess(message);
                    _confirmation = true;
                }
                else
                {
                    var message = await res.Content.ReadAsStringAsync();
                    _toastService.ShowError(message);
                }
            });
        }

        public async Task Confirm()
        {
            await _spinnerService.RunAsync(async () =>
            {
                var res = await _client.PostAsJsonAsync($"auth/reset_password/confirm", new ConfirmResetPasswordPar
                {
                    Email = _username,
                    Otp = _otp,
                    NewPassword = _password
                });

                if (res.IsSuccessStatusCode)
                {
                    _navigationService.Back();
                }
                else
                {
                    var message = await res.Content.ReadAsStringAsync();
                    _toastService.ShowError(message);
                }
            });
        }

        private void showPassword()
        {
            var type = _pwdTypeInput == "password" ? "text" : "password";
            _pwdTypeInput = type;
        }

        private bool Validate()
        {
            return !string.IsNullOrWhiteSpace(_username);
        }

        private bool ValidateConfirm()
        {
            return !string.IsNullOrWhiteSpace(_username)
                && !string.IsNullOrWhiteSpace(_password)
                && !string.IsNullOrWhiteSpace(_otp);
        }
    }
}
