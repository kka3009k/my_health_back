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
    public partial class Registration
    {
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

        public async Task Next()
        {
            await _spinnerService.RunAsync(async () =>
            {

                var res = await _client.PostAsJsonAsync($"registration", new RegistrationPar
                {
                    Email = _username,
                });

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
                var res = await _client.PostAsJsonAsync($"registration/confirm", new ConfirmRegistrationPar
                {
                    Email = _username,
                    Otp = _otp,
                    Password = _password
                });

                if (res.IsSuccessStatusCode)
                {
                    var user = await res.Content.ReadFromJsonAsync<AuthResDto>();
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
