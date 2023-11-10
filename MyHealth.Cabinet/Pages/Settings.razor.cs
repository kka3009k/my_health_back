using Blazored.Modal;
using Blazored.Modal.Services;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using MyHealth.Cabinet.Components;
using MyHealth.Cabinet.Services;
using MyHealth.Data.Dto;
using MyHealth.Data.Dto.Cabinet;
using MyHealth.Data.Entities;
using MyHealth.Data.Utils;
using System.Net.Http.Json;

namespace MyHealth.Cabinet.Pages
{
    public partial class Settings
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

        private UserDto _editUser = new();
        private DoctorUserDto _doctor = new() { Address = new AddressDto() };

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _editUser = _userService.CurrentUser;
                await LoadDoctor();
                StateHasChanged();
            }
        }

        private async Task LoadDoctor()
        {
            await _spinnerService.RunAsync(async () =>
            {
                var res = await _client.GetAsync($"doctorUser/me");

                if (res.IsSuccessStatusCode)
                {
                    _doctor = await res.Content.ReadFromJsonAsync<DoctorUserDto>();
                    _doctor.Address ??= new AddressDto();
                }
                else
                {
                    var message = await res.Content.ReadAsStringAsync();
                    _toastService.ShowError(message);
                }
            });
        }

        private async Task SaveProfile()
        {
            var res = await _client.PostAsJsonAsync($"user/update", _editUser);

            if (res.IsSuccessStatusCode)
            {
                _editUser = await res.Content.ReadFromJsonAsync<UserDto>();
                await SaveDoctor();
            }
            else
            {
                var message = await res.Content.ReadAsStringAsync();
                _toastService.ShowError(message);
            }
        }

        private async Task SaveDoctor()
        {
            var res = await _client.PostAsJsonAsync($"doctorUser/update", _doctor);

            if (res.IsSuccessStatusCode)
            {
                _doctor = await res.Content.ReadFromJsonAsync<DoctorUserDto>();
                _toastService.ShowSuccess("Успешно сохранено");
            }
            else
            {
                var message = await res.Content.ReadAsStringAsync();
                _toastService.ShowError(message);
            }
        }

        private void ShowWarrning()
        {
            Modal.Show<ModalCard>("Внимание!", new ModalParameters()
                    .Add(nameof(ModalCard.Message), "Введенные данные не сохранятся")
                    .Add(nameof(ModalCard.OkClick), EventCallback.Factory.Create(this, _navigationService.Back)));
        }
    }
}
