using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using MyHealth.Cabinet.Services;
using MyHealth.Data.Dto;
using System.Net.Http.Json;

namespace MyHealth.Cabinet.Components
{
    public partial class Menu
    {
        [Parameter]
        public RenderFragment Body { get; set; }

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

        protected override async Task OnAfterRenderAsync(bool pFirstRender)
        {
            if (pFirstRender)
            {
                await LoadUser();
            }
        }

        private async Task LoadUser()
        {
            await _spinnerService.RunAsync(async () =>
            {
                var res = await _client.GetAsync($"user/me");

                if (res.IsSuccessStatusCode)
                {
                    var user = await res.Content.ReadFromJsonAsync<UserDto>();
                    _userService.CurrentUser = user;
                }
                else
                {
                    _toastService.ShowError(await res.Content.ReadAsStringAsync());
                }
            });

            await InvokeAsync(StateHasChanged);
        }

        private void Navigate(string pPath)
        {
            _navigationService.NavigateTo(pPath);
        }
    }
}
