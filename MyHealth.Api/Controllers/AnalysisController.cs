using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyHealth.Api.Extension;
using MyHealth.Api.Service;
using MyHealth.Data;
using MyHealth.Data.Dto;
using MyHealth.Data.Entities;
using System.ComponentModel;
using System.Net;
using System.Reflection;

namespace MyHealth.Api.Controllers
{
    [ApiController]
    [Route("analyzes")]
    public class AnalysisController : ControllerBase
    {
        private readonly ILogger<AnalysisController> _logger;
        private readonly MyDbContext _db;
        private HttpContextService _contextService;
        private readonly FileStorageService _fileStorageService;

        public AnalysisController(ILogger<AnalysisController> logger, MyDbContext pDb, HttpContextService pContextService, FileStorageService pFileStorageService)
        {
            _logger = logger;
            _db = pDb;
            _contextService = pContextService;
            _fileStorageService = pFileStorageService;
        }

        /// <summary>
        /// Получить анализы
        /// </summary>
        /// <param name="pStart">Дата начала</param>
        /// <param name="pEnd">Дата окончаня</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<GetAnalyzesRes>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAnalyzes(DateTime pStart, DateTime pEnd)
        {
            var userId = _contextService.UserId();
            var hasUser = await _db.Users.AnyAsync(f => f.ID == userId);

            if (!hasUser)
                return BadRequest("Пользователь не найден");

            var analyzes = await _db.Analyzes.Where(w => w.UserID == userId && w.Date >= pStart && w.Date <= pEnd).Select(s => new GetAnalyzesRes
            {
                ID = s.ID,
                AnalysisTypeName = s.AnalysisType.Name,
                Date = s.Date,
            }).ToListAsync();

            return Ok(analyzes);
        }

        /// <summary>
        /// Получить анализ
        /// </summary>
        /// <param name="id">Код анализа</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AnalysisDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAnalysis(int id)
        {
            var userId = _contextService.UserId();
            var hasUser = await _db.Users.AnyAsync(f => f.ID == userId);

            if (!hasUser)
                return BadRequest("Пользователь не найден");

            var analysis = await _db.Analyzes.FirstOrDefaultAsync(f => f.ID == id);

            if (analysis == null)
                return BadRequest("Анализ не найден");

            var analysisDto = new AnalysisDto();
            analysisDto.ID = analysis.ID;
            analysisDto.LaboratoryID = analysis.LaboratoryID;
            analysisDto.AnalysisTypeID = analysis.AnalysisTypeID;
            analysisDto.Date = analysis.Date;
            analysisDto.ExtraInfo = analysis.ExtraInfo;
            analysisDto.Price = analysis.Price;

            var analysisFile = await _db.AnalysisFiles.FirstOrDefaultAsync(f => f.AnalysisID == id);
            analysisDto.File = await _fileStorageService.GetFilePathAsync(analysisFile?.FileID);

            return Ok(analysisDto);
        }


        /// <summary>
        /// Добавить анализ
        /// </summary>
        /// <param name="pAnalysis"></param>
        /// <returns></returns>
        [HttpPost("add")]
        [ProducesResponseType(typeof(AnalysisDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddAnalysis([FromForm] AddAnalysisPar pAnalysis)
        {
            var userId = _contextService.UserId();
            var hasUser = await _db.Users.AnyAsync(f => f.ID == userId);

            if (!hasUser)
                return BadRequest("Пользователь не найден");

            var analysis = new Analysis();
            analysis.AnalysisTypeID = pAnalysis.AnalysisTypeID;
            analysis.LaboratoryID = pAnalysis.LaboratoryID;
            analysis.Date = pAnalysis.Date;
            analysis.Price = pAnalysis.Price;
            analysis.ExtraInfo = pAnalysis.ExtraInfo;
            analysis.UserID = userId;

            await _db.AddAsync(analysis);

            await _db.SaveChangesAsync();

            if (pAnalysis.File != null)
            {
                var file = await _fileStorageService.SaveFileAsync(pAnalysis.File);
                var analysisFile = new AnalysisFile
                {
                    AnalysisID = analysis.ID,
                    FileID = file.ID
                };

                await _db.AddAsync(analysisFile);
                await _db.SaveChangesAsync();
            }

            var analysisDto = await GetAnalysis(analysis.ID);
            return Ok(analysisDto);
        }

        /// <summary>
        /// Обновить анализ
        /// </summary>
        /// <param name="pAnalysis"></param>
        /// <returns></returns>
        [HttpPut("update")]
        [ProducesResponseType(typeof(AnalysisDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateAnalysis([FromForm] UpdAnalysisPar pAnalysis)
        {
            var userId = _contextService.UserId();
            var hasUser = await _db.Users.AnyAsync(f => f.ID == userId);

            if (!hasUser)
                return BadRequest("Пользователь не найден");


            var analysis = await _db.Analyzes.FirstOrDefaultAsync(f => f.ID == pAnalysis.ID);

            if (analysis == null)
                return BadRequest("Анализ не найден");

            analysis.FillField(analysis.AnalysisTypeID, pAnalysis.AnalysisTypeID, f => analysis.AnalysisTypeID = pAnalysis.AnalysisTypeID);
            analysis.FillField(analysis.LaboratoryID, pAnalysis.LaboratoryID, f => analysis.LaboratoryID = pAnalysis.LaboratoryID);
            analysis.FillField(analysis.Date, pAnalysis.Date, f => analysis.Date = pAnalysis.Date);
            analysis.FillField(analysis.Price, pAnalysis.Price, f => analysis.Price = pAnalysis.Price);
            analysis.FillField(analysis.ExtraInfo, pAnalysis.ExtraInfo, f => analysis.ExtraInfo = pAnalysis.ExtraInfo);

            await _db.SaveChangesAsync();

            if (pAnalysis.File != null)
            {
                var file = await _fileStorageService.SaveFileAsync(pAnalysis.File);

                var analysisFile = new AnalysisFile
                {
                    AnalysisID = analysis.ID,
                    FileID = file.ID
                };

                await _db.AddAsync(analysisFile);
                await _db.SaveChangesAsync();
                await _db.AnalysisFiles.Where(w => w.AnalysisID == analysis.ID && w.FileID != file.ID).ExecuteDeleteAsync();
            }

            var analysisDto = await GetAnalysis(analysis.ID);
            return Ok(analysisDto);
        }

        /// <summary>
        /// Удалить анализ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnalysis(int id)
        {
            var analysis = await _db.Analyzes.FirstOrDefaultAsync(f => f.ID == id);

            if (analysis == null)
                return BadRequest("Анализ не найден");

            _db.Remove(analysis);
            await _db.SaveChangesAsync();

            return Ok();
        }
    }
}