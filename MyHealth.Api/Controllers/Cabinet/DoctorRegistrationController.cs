using Microsoft.AspNetCore.Mvc;

namespace MyHealth.Api.Controllers.Cabinet
{
    public class DoctorRegistrationController : Controller
    {
        public IActionResult Registration()
        {
            return View();
        }
    }
}
