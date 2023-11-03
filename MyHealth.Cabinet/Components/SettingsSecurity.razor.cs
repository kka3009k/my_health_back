using Blazored.Modal;
using Blazored.Modal.Services;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using MyHealth.Cabinet.Components;
using MyHealth.Cabinet.Services;
using MyHealth.Data.Dto;
using MyHealth.Data.Dto.Cabinet;
using System.Net.Http.Json;

namespace MyHealth.Cabinet.Components
{
    public partial class SettingsSecurity
    {
        [CascadingParameter]
        public IModalService Modal { get; set; } = default!;

        [Inject]
        private UserStateService _userService { get; set; }

        [Inject]
        private SpinnerService _spinnerService { get; set; }

        [Inject]
        private HttpClient _client { get; set; }

        [Inject]
        private IToastService _toastService { get; set; }

        [Inject]
        private NavigationService _navigationService { get; set; }

        private ChangePasswordPar _changePasswordPar = new();

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await InvokeAsync(StateHasChanged);
            }
        }

        private async Task Change()
        {
            var res = await _client.PutAsJsonAsync($"user/change_password", _changePasswordPar);

            if (res.IsSuccessStatusCode)
            {
                _toastService.ShowSuccess("Пароль успешно обновлен");
                _changePasswordPar = new();
                await InvokeAsync(StateHasChanged);
            }
            else
            {
                var message = await res.Content.ReadAsStringAsync();
                _toastService.ShowError(message);
            }
        }

        private bool Valiade() => !string.IsNullOrWhiteSpace(_changePasswordPar.Password)
            && !string.IsNullOrWhiteSpace(_changePasswordPar.NewPassword);

        private void ShowWarrning()
        {
            Modal.Show<ModalCard>("Внимание!", new ModalParameters()
                    .Add(nameof(ModalCard.Message), "Введенные данные не сохранятся")
                    .Add(nameof(ModalCard.OkClick), EventCallback.Factory.Create(this, _navigationService.Back)));
        }
    }
}
