using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using RealEstateWebApp.DataAccess.Repositories;
using RealEstateWebApp.DataAccess.Repositories.Files;
using RealEstateWebApp.Models.Files;
using HeyRed.Mime;
using Microsoft.Extensions.Configuration;

namespace RealEstateWebApp.Business
{
    public class FileService
    {
        private IFileRepository _fileRepository;
        private readonly IConfiguration _configuration;
        private string _storagePath;

        public FileService(IConfiguration configuration, IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
            _configuration = configuration;

            PrepareFileStorage();
        }

        public async Task<bool> SaveFile(RecordsFileModel fileInfo)
        {
            if(fileInfo.Id != 0)
                return await _fileRepository.Update(fileInfo.Id, fileInfo.IsDeleted);

            var fileName = Guid.NewGuid().ToString() + '.' + GetExtension(fileInfo.ContentType);
            var relativePath = Path.Combine(fileInfo.CategoryId.ToString(), fileInfo.RecordId.ToString());
            var fileDb = PrepareDbFileRecord(fileInfo, Path.Combine(relativePath, fileName));

            var res = await _fileRepository.Create(fileDb);
            if (!res)
                return false;

            var destinationPath = Path.Combine(_storagePath, relativePath);
            if (!Directory.Exists(destinationPath))
                Directory.CreateDirectory(destinationPath);
            await File.WriteAllBytesAsync(Path.Combine(destinationPath, fileName), fileInfo.FileData);

            return true;
        }

        
        public Task<IEnumerable<FileViewModel>> GetByRecordId(int recordId)
        {
            return _fileRepository.GetByRecordId(recordId);
        }

        private void PrepareFileStorage()
        {
            if (string.IsNullOrWhiteSpace(_configuration["FileStoragePath"]))
                throw new NullReferenceException("Could not find configuration for file storage");


            _storagePath = Path.GetFullPath(_configuration["FileStoragePath"]);
            if (Directory.Exists(_storagePath))
                return;
            Directory.CreateDirectory(_storagePath);
        }

        private FileCreateModel PrepareDbFileRecord(RecordsFileModel info, string relativePath)
        {
            return new FileCreateModel()
            {
                RecordId = info.RecordId,
                FilePath = relativePath,
                FileName = info.FileName,
                ContentType = info.ContentType,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = info.UserId
            };
        }

        private static string GetExtension(string contentType)
        {
            return MimeTypesMap.GetExtension(contentType);
        }

        private string GetFilePath(string relativePath)
        {
            return Path.Combine(_storagePath, relativePath);
        }

        public byte[] GetUserImage(int userId)
        {
            var filePath = GetFilePath($"userAvatar/{userId}.png");
            if (!File.Exists(filePath))
                return null;
            return File.ReadAllBytes(filePath);
        }

        public async Task<byte[]> GetByIdAndRecordId(int recordId ,int id)
        {
            var filePath = await _fileRepository.GetByIdAndRecordId(recordId, id);
            byte[] res = null;
            var path = GetFilePath(filePath);
            if (File.Exists(path))
            {
                res = File.ReadAllBytes(path);
            }
            return res;
        }

        public void DeleteRecordFiles(int recordId, int categoryId)
        {
            var relativePath = Path.Combine(categoryId.ToString(), recordId.ToString());
            DirectoryInfo directory = new DirectoryInfo(GetFilePath(relativePath));
            foreach (FileInfo file in directory.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in directory.GetDirectories())
            {
                dir.Delete(true);
            }
        }
    }
}