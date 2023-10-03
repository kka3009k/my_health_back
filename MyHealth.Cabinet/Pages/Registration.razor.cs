using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace MyHealth.Cabinet.Pages
{
    public partial class Registration
    { 


        [Inject] private HttpClient httpClient { get; set; }
        public async void UserRegistration()
        {
            //var response = await httpClient.PostAsJsonAsync("/registration", );
            //var a  = await response.Content.ReadFromJsonAsync();
        }
    }
}
