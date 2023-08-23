using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MyHealth.AdminPanel.Services;
using System.Runtime.CompilerServices;

namespace MyHealth.AdminPanel.Pages
{
    public partial class Login
    {
        [Inject]
        private UserStateService _userStateService { get; set; }

        private string _username;
        private string _password;

        private async Task SignIn()
        {
            var token = "sdsd";

            await _userStateService.SignInAsync(token);
        }

        private bool Validate()
        {
            return !string.IsNullOrWhiteSpace(_username) && !string.IsNullOrEmpty(_password);
        }

        private async Task OnKeyDown(KeyboardEventArgs pArgs)
        {
            if (IsEnter(pArgs.Code) && Validate()) await SignIn();
        }

        private bool IsEnter(string pCode)
            => new string[] { "Enter", "NumpadEnter" }.Contains(pCode);
    }
}
