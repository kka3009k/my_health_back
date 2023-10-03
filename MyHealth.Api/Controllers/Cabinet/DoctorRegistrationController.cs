using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyHealth.Data;
using MyHealth.Data.Dto;
using MyHealth.Data.Entities;

namespace MyHealth.Api.Controllers.Cabinet
{
    public class DoctorRegistrationController : Controller
    {
        //private readonly MyDbContext _db;
        //public IActionResult Registration()
        //{
        //    var userExists =  _db.Users.Any(a => a.Phone == pUser.Phone);

        //    if (userExists)
        //        return BadRequest("Пользователь существует");


        //    return View();
        //}

        //public async Task<IActionResult> Registration(UserRegistrationDto pUser)
        //{
           

        //    var user = new User
        //    {
        //        FirstName = pUser.FirstName,
        //        LastName = pUser.LastName,
        //        Role = RoleTypes.Client,
        //        Email = pUser.Email,
        //        Phone = pUser.Phone,
        //        PasswordHash = ComputeSha256Hash(pUser.Password)
        //    };

        //    await _db.AddAsync(user);
        //    await _db.SaveChangesAsync();

        //    return Ok();
        //}
    }
}
