using Firebase.Auth.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyHealth.Api.Extension;
using MyHealth.Api.Service;
using MyHealth.Api.Static;
using MyHealth.Data;
using MyHealth.Data.Dto;
using MyHealth.Data.Entities;
using System.ComponentModel;
using System.Net;
using System.Reflection;

namespace MyHealth.Api.Controllers
{
    [ApiController]
    [Route("symptoms")]
    public class SymptomController : ControllerBase
    {
        private readonly ILogger<SymptomController> _logger;
        private readonly MyDbContext _db;
        private HttpContextService _contextService;
        private readonly FileStorageService _fileStorageService;

        public SymptomController(ILogger<SymptomController> logger, MyDbContext pDb, HttpContextService pContextService, FileStorageService pFileStorageService)
        {
            _logger = logger;
            _db = pDb;
            _contextService = pContextService;
            _fileStorageService = pFileStorageService;
        }

        /// <summary>
        /// Получить симптомы
        /// </summary>
        /// <param name="pStart">Дата начала</param>
        /// <param name="pEnd">Дата окончаня</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<GetSymptomsRes>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetSymptoms(DateTime pStart, DateTime pEnd)
        {
            var userId = _contextService.UserId();

            var analyzes = await _db.Symptoms.Where(w => w.UserID == userId && w.Date >= pStart && w.Date <= pEnd).Select(s => new GetSymptomsRes
            {
                ID = s.ID,
                Description = s.Description,
                Date = s.Date,
            }).ToListAsync();

            return Ok(analyzes);
        }

        /// <summary>
        /// Получить симптом
        /// </summary>
        /// <param name="id">Код симптома</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SymptomDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetSymptom(int id)
        {
            var userId = _contextService.UserId();

            var symptomDto = await _db.Symptoms
                .Where(w => w.ID == id)
                .Select(s => new SymptomDto
                {
                    ID = s.ID,
                    Description = s.Description,
                    Date = s.Date,
                    Files = _db.SymptomFiles.Where(w => w.SymptomID == id).Select(s => new FileDto
                    {
                        ID = s.FileID,
                        Extension = s.File.Extension,
                        Name = s.File.Name,
                        Path = $"{Constants.FileStorageName}/{s.FileID}.{s.File.Extension}"
                    }).ToList()
                }).FirstOrDefaultAsync();

            if (symptomDto == null)
                return BadRequest("Симптом не найден");

            return Ok(symptomDto);
        }


        /// <summary>
        /// Добавить симптом
        /// </summary>
        /// <param name="pSymptom"></param>
        /// <returns></returns>
        [HttpPost("add")]
        [ProducesResponseType(typeof(SymptomDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddSymptom([FromForm] AddSymptomPar pSymptom)
        {
            var userId = _contextService.UserId();

            var symptom = new Symptom();
            symptom.Description = pSymptom.Description;
            symptom.Date = pSymptom.Date;
            symptom.UserID = userId;

            await _db.AddAsync(symptom);

            await _db.SaveChangesAsync();

            await SaveFiles(pSymptom.Files, symptom.ID);

            var symptomDto = await GetSymptom(symptom.ID);
            return Ok(symptomDto);
        }

        /// <summary>
        /// Обновить симптом
        /// </summary>
        /// <param name="pSymptom"></param>
        /// <returns></returns>
        [HttpPut("update")]
        [ProducesResponseType(typeof(SymptomDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateSymptom([FromForm] UpdSymptomPar pSymptom)
        {
            var userId = _contextService.UserId();
            var symptom = await _db.Symptoms.FirstOrDefaultAsync(f => f.ID == pSymptom.ID);

            if (symptom == null)
                return BadRequest("Симптом не найден");

            symptom.FillField(symptom.Description, pSymptom.Description, f => symptom.Description = pSymptom.Description);
            symptom.FillField(symptom.Date, pSymptom.Date, f => symptom.Date = pSymptom.Date);

            await _db.SaveChangesAsync();

            await SaveFiles(pSymptom.Files, symptom.ID);

            var symptomDto = await GetSymptom(symptom.ID);
            return Ok(symptomDto);
        }

        /// <summary>
        /// Удалить симптом
        /// </summary>
        /// <param name="id">Код симптома</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnalysis(int id)
        {
            var symptom = await _db.Symptoms.FirstOrDefaultAsync(f => f.ID == id);

            if (symptom == null)
                return BadRequest("Анализ не найден");

            _db.Remove(symptom);
            await _db.SaveChangesAsync();

            return Ok();
        }


        /// <summary>
        /// Удалить файл симптома
        /// </summary>
        /// <param name="id">Код файла</param>
        /// <returns></returns>
        [HttpDelete("file/{id}")]
        public async Task<IActionResult> DeleteSymptomFile(int id)
        {
            var symptomFile = await _db.SymptomFiles.FirstOrDefaultAsync(f => f.FileID == id);

            if (symptomFile == null)
                return BadRequest("Файл не найден");

            _db.Remove(symptomFile);
            await _db.SaveChangesAsync();

            return Ok();
        }


        private async Task SaveFiles(List<IFormFile> pFiles, int SymptomID)
        {
            if (pFiles == null)
                return;

            foreach (var formFile in pFiles)
            {
                var file = await _fileStorageService.SaveFileAsync(formFile);

                var symptomFile = new SymptomFile
                {
                    SymptomID = SymptomID,
                    FileID = file.ID
                };

                await _db.AddAsync(symptomFile);
                await _db.SaveChangesAsync();
            }
        }
    }
}