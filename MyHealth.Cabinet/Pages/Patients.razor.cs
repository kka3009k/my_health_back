using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using MyHealth.Cabinet.Components;
using MyHealth.Cabinet.Services;
using MyHealth.Data.Dto;
using MyHealth.Data.Dto.Cabinet;
using System.Net.Http.Json;

namespace MyHealth.Cabinet.Pages
{
    public partial class Patients
    {
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

        private List<PatientAllDto> _patients;
        private PatientCreate _patientCreateModal;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await LoadPatient();
                StateHasChanged();
            }
        }

        private async Task LoadPatient()
        {
            await _spinnerService.RunAsync(async () =>
            {
                var res = await _client.GetAsync($"patient/all");

                if (res.IsSuccessStatusCode)
                    _patients = await res.Content.ReadFromJsonAsync<List<PatientAllDto>>();
                else
                {
                    var message = await res.Content.ReadAsStringAsync();
                    _toastService.ShowError(message);
                }
            });
        }

        private void Add()
        {
            _patientCreateModal.Create();
        }
    }
}
