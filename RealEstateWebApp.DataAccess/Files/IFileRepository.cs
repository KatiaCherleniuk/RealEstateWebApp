using System.Collections.Generic;
using System.Threading.Tasks;
using RealEstateWebApp.Models.Files;

namespace RealEstateWebApp.DataAccess.Repositories.Files
{
    public interface IFileRepository
    {
        Task<bool> Create(FileCreateModel file);
        Task<bool> Update(int fileId, bool isDeleted, bool isMain);
        Task<IEnumerable<FileViewModel>> GetByRecordId(int recordId);
        Task<string> GetByIdAndRecordId(int recordId, int id);
        Task<string> GetRecordMainImage(int recordId);
    }
}