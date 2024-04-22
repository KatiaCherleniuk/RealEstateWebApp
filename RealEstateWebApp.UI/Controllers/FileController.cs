using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateWebApp.Business;
using RealEstateWebApp.Business.Identity;

namespace RealEstateWebApp.UI.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class FileController : Controller
    {
        private readonly FileService _fileService;
        private readonly ISessionUserResolver _sessionUserResolver;

        public FileController(FileService fileService, ISessionUserResolver sessionUserResolver)
        {
            _fileService = fileService;
            _sessionUserResolver = sessionUserResolver;
        }

        [HttpGet]
        public async Task<IActionResult> GetByIdAndRecordId(int recordId, int fileId)
        {
            var resImage = await _fileService.GetByIdAndRecordId(recordId, fileId);
            if (resImage == null || resImage.Length == 0)
                return File("/images/user.png", "image/png", "avatar.png");
            return File(resImage, "image/png");
        }
        [HttpGet("/File/GetFirstByRecordId/{recordId}")]
        public async Task<IActionResult> GetFirstByRecordId(int recordId)
        {
            var files = await _fileService.GetByRecordId(recordId); // by priority!!
            if (files == null || !files.Any())
                return File("~/img/record_no_image.jpg", "image/png");
            var firstFile = files.First();
            return File(firstFile.FileName, "image/png");
        }


        [HttpGet]
        public Task<IActionResult> GetUserImage()
        {
            if (!_sessionUserResolver.IsAuthorized)
                return Task.FromResult<IActionResult>(Forbid());
            return GetUserImage(_sessionUserResolver.User.Id);
        }


        [HttpGet("/File/GetUserImage/{userId}")]
        public async Task<IActionResult> GetUserImage(int userId)
        {
            var resImage = _fileService.GetUserImage(userId);
            if (resImage == null || resImage.Length == 0)
                return File("~/img/user.png", "image/png", "avatar.png");
            return File(resImage, "image/png", "avatar.png");
        }
    }
}
