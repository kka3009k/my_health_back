using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace MyHealth.Cabinet.Pages
{
    public partial class Index
    { 


        [Inject] private HttpClient httpClient { get; set; }
        [Inject] private NavigationManager NavManager { get; set; }
        public async void UserRegistration()
        {
            NavManager.NavigateTo("/Registration");
        }
    }
}
