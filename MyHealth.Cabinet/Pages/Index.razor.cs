using Microsoft.AspNetCore.Components;

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
