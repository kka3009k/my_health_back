using Microsoft.AspNetCore.Components;
using MyHealth.Data.Entities;
using System.Net.Http.Json;

namespace MyHealth.Cabinet.Pages
{
    public partial class Registration
    {


        [Inject] private HttpClient httpClient { get; set; }
        User _doctor = new User();
        public async void UserRegistration()
        {
            //var response = await httpClient.PostAsJsonAsync("/registration", );
            //var a  = await response.Content.ReadFromJsonAsync();
        }

        public void CreateDoctor()
        {
            var a = httpClient.PostAsJsonAsync($"api/DoctorRegistration", _doctor);
        }
    }
}
