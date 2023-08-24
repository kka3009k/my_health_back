using DotNetEnv;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MyHealth.Admin.Components;
using MyHealth.Admin.Services;
using MyHealth.Data.Dto;
using Radzen;
using System.Runtime.CompilerServices;

namespace MyHealth.Admin.Pages
{
    public partial class Login
    {
        [Inject]
        private UserStateService _userStateService { get; set; }

        [Inject]
        private SpinnerService _spinner { get; set; }

        [Inject]
        IHttpClientFactory _clientFactory { get; set; }

        [Inject]
        private NotificationService _notification { get; set; }

        private string _email;
        private string _password;

        private async Task SignIn()
        {
            await _spinner.RunAsync(async () =>
             {
                 var client = _clientFactory.CreateClient();
                 client.BaseAddress = new Uri(Env.GetString("BACKEND_URL"));
                 var result = await client.PostAsJsonAsync("auth/email", new EmailAuthPar
                 {
                     Email = _email,
                     Password = _password
                 });

                 if (result.IsSuccessStatusCode)
                 {
                     var authRes = await result.Content.ReadFromJsonAsync<AuthResDto>();
                     await _userStateService.SignInAsync(authRes.AccessToken);
                 }
                 else
                 {
                     var message = await result.Content.ReadAsStringAsync();
                     _notification.Notify(NotificationSeverity.Error, message);
                 }
             });
        }

        private bool Validate()
        {
            return !string.IsNullOrWhiteSpace(_email) && !string.IsNullOrEmpty(_password);
        }

        private async Task OnKeyDown(KeyboardEventArgs pArgs)
        {
            if (IsEnter(pArgs.Code) && Validate()) await SignIn();
        }

        private bool IsEnter(string pCode)
            => new string[] { "Enter", "NumpadEnter" }.Contains(pCode);
    }
}
