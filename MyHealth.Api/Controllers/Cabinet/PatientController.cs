using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyHealth.Api.Extension;
using MyHealth.Api.Service;
using MyHealth.Api.Utils;
using MyHealth.Data;
using MyHealth.Data.Dto;
using MyHealth.Data.Dto.Cabinet;
using MyHealth.Data.Entities;
using MyHealth.Data.Entities.DoctorCabinet;
using System.Net;
using System.Numerics;

namespace MyHealth.Api.Controllers.Cabinet
{
    /// <summary>
    /// Контроллер для взаимодействия с данными врача
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class PatientController : ControllerBase
    {
        private readonly ILogger<PatientController> _logger;
        private readonly MyDbContext _db;
        private UserContextService _contextService;
        private readonly FileStorageService _fileStorageService;

        public PatientController(ILogger<PatientController> logger, MyDbContext pDb, UserContextService pContextService, FileStorageService pFileStorageService)
        {
            _logger = logger;
            _db = pDb;
            _contextService = pContextService;
            _fileStorageService = pFileStorageService;
        }

        /// <summary>
        /// Получить пациентов
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        [ProducesResponseType(typeof(List<PatientAllDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAll()
        {
            var userId = _contextService.UserId();
            var patients = await _db.Patients
                .Include(i => i.User)
                .Where(w => w.DoctorUser.UserID == userId)
                .ToListAsync();

            return Ok(patients.Select(s => new PatientAllDto
            {
                PatientID = s.ID,
                FirstName = s.User?.FirstName,
                LastName = s.User?.LastName,
                Patronymic = s.User?.Patronymic,
                Phone = s.User?.Phone,
                Email = s.User?.Email,
            }).ToList());
        }

        /// <summary>
        /// Получить пациента
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PatientDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await LoadPatient(id));
        }

        /// <summary>
        /// Добавить пациента
        /// </summary>
        /// <param name="pPatient"></param>
        /// <returns></returns>
        [HttpPost()]
        [ProducesResponseType(typeof(PatientDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] PatientDto pPatient)
        {
            var userId = _contextService.UserId();
            var doctorId = await _db.DoctorUsers
                .Where(w=>w.UserID == userId)
                .Select(s=>s.ID)
                .FirstOrDefaultAsync();

            var patient = new Patient
            {
                DoctorID = doctorId,
                User = new User
                {
                    INN = pPatient.User?.INN,
                    FirstName = pPatient.User?.FirstName,
                    LastName = pPatient.User?.LastName,
                    Patronymic = pPatient.User?.Patronymic,
                    Gender = pPatient.User?.Gender,
                    Phone = pPatient.User?.Phone,
                    Email = pPatient.User?.Email,
                    BirthDate = pPatient.User?.BirthDate,
                },
                MothersName = pPatient.MothersName,
                FathersName = pPatient.FathersName,
                Address = new Address
                {
                    City = pPatient.Address?.City,
                    House = pPatient.Address?.House,
                    Region = pPatient.Address?.Region,
                    Street = pPatient.Address?.Street,
                }
            };

            await _db.AddAsync(patient);
            await _db.SaveChangesAsync();

            return Ok(await LoadPatient(patient.ID));
        }

        /// <summary>
        /// Обновить пациента
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pPatient"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(PatientDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update(Guid id, [FromBody] PatientDto pPatient)
        {
            var userId = _contextService.UserId();
            var patient = await _db.Patients
                .Include(i => i.User)
                .Include(i => i.DoctorUser)
                .FirstOrDefaultAsync(w => w.ID == id);

            patient.Address ??= new Address();

            patient.FillField(patient.MothersName, pPatient.MothersName, v => patient.MothersName = v);
            patient.FillField(patient.FathersName, pPatient.FathersName, v => patient.FathersName = v);

            patient.FillField(patient.User.INN, pPatient.User.INN, v => patient.User.INN = v);
            patient.FillField(patient.User.FirstName, pPatient.User.FirstName, v => patient.User.FirstName = v);
            patient.FillField(patient.User.LastName, pPatient.User.LastName, v => patient.User.LastName = v);
            patient.FillField(patient.User.Patronymic, pPatient.User.Patronymic, v => patient.User.Patronymic = v);
            patient.FillField(patient.User.Gender, pPatient.User.Gender, v => patient.User.Gender = v);
            patient.FillField(patient.User.Email, pPatient.User.Email, v => patient.User.Email = v);
            patient.FillField(patient.User.Phone, pPatient.User.Phone, v => patient.User.Phone = v);

            patient.FillField(patient.Address.Region, pPatient.Address.Region, v => patient.Address.Region = v);
            patient.FillField(patient.Address.City, pPatient.Address.City, v => patient.Address.City = v);
            patient.FillField(patient.Address.Street, pPatient.Address.Street, v => patient.Address.Street = v);
            patient.FillField(patient.Address.House, pPatient.Address.House, v => patient.Address.House = v);

            await _db.SaveChangesAsync();
            return Ok(await LoadPatient(id));
        }

        private async Task<PatientDto> LoadPatient(Guid pPatientID)
        {
            var patient = await _db.Patients
                .Include(i => i.User)
                .Include(i => i.DoctorUser)
                .FirstOrDefaultAsync(w => w.ID == pPatientID);

            return new PatientDto
            {
                User = new UserDto
                {
                    INN = patient.User?.INN,
                    FirstName = patient.User?.FirstName,
                    LastName = patient.User?.LastName,
                    Patronymic = patient.User?.Patronymic,
                    Gender = patient.User?.Gender,
                    Phone = patient.User?.Phone,
                    Email = patient.User?.Email,
                    BirthDate = patient.User?.BirthDate,
                },
                MothersName = patient?.MothersName,
                FathersName = patient?.FathersName,
                Address = new AddressDto
                {
                    AddressID = patient?.Address?.ID,
                    City = patient?.Address?.City,
                    House = patient?.Address?.House,
                    Region = patient?.Address?.Region,
                    Street = patient?.Address?.Street,
                }
            };
        }
    }
}