using Microsoft.EntityFrameworkCore;
using MyHealth.Api.Static;
using MyHealth.Data;
using MyHealth.Data.Entities;

namespace MyHealth.Api.Service
{
    /// <summary>
    /// Обработчик файлов пользователя
    /// </summary>
    public class FileStorageService
    {
        private readonly MyDbContext _db;

        public FileStorageService(MyDbContext pDb)
        {
            _db = pDb;
        }

        public async Task<FileStorage> SaveFileAsync(IFormFile pFile)
        {
            var fileInfo = pFile.FileName.Split('.');
            var file = new FileStorage
            {
                Name = fileInfo[0],
                Extension = fileInfo.Length > 1 ? fileInfo[fileInfo.Length - 1] : string.Empty,
            };

            await _db.AddAsync(file);
            await _db.SaveChangesAsync();

            using (var stream = new FileStream(Path.Combine(Constants.FileStoragePath, $"{file.ID}.{file.Extension}"), FileMode.Create))
            {
                await pFile.CopyToAsync(stream);
            }

            return file;
        }

        public async Task<string> GetFilePathAsync(Guid? pFileID)
        {
            if (pFileID == null)
                return string.Empty;

            var file = await _db.FileStorages.FirstOrDefaultAsync(f => f.ID == pFileID);

            return GetFilePath(file);
        }

        public string GetFilePath(FileStorage pFile)
        {
            if (pFile == null)
                return string.Empty;

            return $"{Constants.FileStorageName}/{pFile.ID}.{pFile.Extension}";
        }
    }
}
