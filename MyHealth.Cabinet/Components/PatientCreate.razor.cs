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
    public partial class PatientCreate
    {
        [Parameter]
        public EventCallback OnSave { get; set; }

        [Inject]
        private SpinnerService _spinnerService { get; set; }

        [Inject]
        private HttpClient _client { get; set; }

        [Inject]
        private IToastService _toastService { get; set; }

        private PatientDto _patient;
        private bool _visible;

        public void Create()
        {
            _patient = new() { Address = new AddressDto(), User = new UserDto() };
            _visible = true;
        }

        private void Cancel() => _visible = false;

        private bool Validate() =>
            (!string.IsNullOrWhiteSpace(_patient.User.FirstName) || !string.IsNullOrWhiteSpace(_patient.User.LastName))
            && (!string.IsNullOrWhiteSpace(_patient.User.Phone) || !string.IsNullOrWhiteSpace(_patient.User.Email));

        private async Task Save()
        {
            var res = await _client.PostAsJsonAsync($"patient", _patient);

            if (res.IsSuccessStatusCode)
            {
                _patient = await res.Content.ReadFromJsonAsync<PatientDto>();
                _toastService.ShowSuccess("Успешно сохранено");
                Cancel();
                await OnSave.InvokeAsync();
            }
            else
            {
                var message = await res.Content.ReadAsStringAsync();
                _toastService.ShowError(message);
            }
        }
    }
}
