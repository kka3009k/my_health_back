using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyHealth.Api.Extension;
using MyHealth.Api.Service;
using MyHealth.Api.Utils;
using MyHealth.Data;
using MyHealth.Data.Dto;
using MyHealth.Data.Dto.Cabinet;
using MyHealth.Data.Entities.DoctorCabinet;
using System.Net;

namespace MyHealth.Api.Controllers.Cabinet
{
    /// <summary>
    /// Контроллер для взаимодействия с данными врача
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class DoctorUserController : ControllerBase
    {
        private readonly ILogger<DoctorUserController> _logger;
        private readonly MyDbContext _db;
        private UserContextService _contextService;
        private readonly FileStorageService _fileStorageService;

        public DoctorUserController(ILogger<DoctorUserController> logger, MyDbContext pDb, UserContextService pContextService, FileStorageService pFileStorageService)
        {
            _logger = logger;
            _db = pDb;
            _contextService = pContextService;
            _fileStorageService = pFileStorageService;
        }

        /// <summary>
        /// Получить данные врача
        /// </summary>
        /// <returns></returns>
        [HttpGet("me")]
        [ProducesResponseType(typeof(DoctorUserDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetDoctor()
        {
            var userId = _contextService.UserId();
            return Ok(await LoadDoctor(userId));
        }

        /// <summary>
        /// Обновить данные доктора
        /// </summary>
        /// <param name="pDoctor"></param>
        /// <returns></returns>
        [HttpPost("update")]
        [ProducesResponseType(typeof(DoctorUserDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateDoctor([FromBody] DoctorUserDto pDoctor)
        {
            var userId = _contextService.UserId();
            var doctor = await _db.DoctorUsers.Include(i => i.Address).FirstOrDefaultAsync(f => f.UserID == userId);
            doctor.FillField(doctor.Diplomas, pDoctor.Diplomas, v => doctor.Diplomas = v);
            doctor.FillField(doctor.Speciality, pDoctor.Speciality, v => doctor.Speciality = v);

            doctor.Address ??= new();

            if (pDoctor.Address != null)
            {
                doctor.FillField(doctor.Address.Region, pDoctor.Address.Region, v => doctor.Address.Region = v);
                doctor.FillField(doctor.Address.City, pDoctor.Address.City, v => doctor.Address.City = v);
                doctor.FillField(doctor.Address.Street, pDoctor.Address.Street, v => doctor.Address.Street = v);
                doctor.FillField(doctor.Address.House, pDoctor.Address.House, v => doctor.Address.House = v);
            }


            await _db.SaveChangesAsync();

            return Ok(await LoadDoctor(userId));
        }

        private async Task<DoctorUserDto> LoadDoctor(Guid pUserID)
        {
            var doctor = await _db.DoctorUsers.Include(i => i.Address).FirstOrDefaultAsync(f => f.UserID == pUserID);

            if (doctor == null)
            {
                doctor = new DoctorUser { UserID = pUserID };
                await _db.AddAsync(doctor);
                await _db.SaveChangesAsync();
            }

            var doctorDto = new DoctorUserDto()
            {
                Diplomas = doctor.Diplomas,
                Speciality = doctor.Speciality,
                Address = new AddressDto
                {
                    Region = doctor.Address?.Region,
                    AddressID = doctor.AddressID,
                    City = doctor.Address?.City,
                    House = doctor.Address?.House,
                    Street = doctor.Address?.Street,
                }
            };

            return doctorDto;
        }
    }
}