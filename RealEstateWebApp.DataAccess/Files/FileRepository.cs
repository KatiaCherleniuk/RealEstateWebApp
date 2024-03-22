using System.Collections.Generic;
using System.Threading.Tasks;
using RealEstateWebApp.Models.Files;
using Microsoft.Extensions.Configuration;

namespace RealEstateWebApp.DataAccess.Repositories.Files
{
    public class FileRepository: DataController, IFileRepository
    {
        public FileRepository(IConfiguration configuration) : base(configuration, "Files")
        {
        }

        public Task<bool> Create(FileCreateModel file)
        {
            return InsertWithIdAsync(file);
        }

        public Task<bool> Update(int fileId, bool isDeleted)
        {
            return PerformNonQuery("Update", new { Id = fileId, IsDeleted = isDeleted });
        }

        public Task<IEnumerable<FileViewModel>> GetByRecordId(int recordId)
        {
            return GetManyAsync<FileViewModel>("GetByRecordId", new {RecordId = recordId});
        }

        public Task<string> GetByIdAndRecordId(int recordId, int id)
        {
            return GetOneAsync<string>("GetByIdAndRecordId", new { Id = id, RecordId = recordId });
        }
    }
}