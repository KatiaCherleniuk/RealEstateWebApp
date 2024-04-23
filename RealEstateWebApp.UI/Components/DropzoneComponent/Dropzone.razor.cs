using RealEstateWebApp.Business;
using RealEstateWebApp.Models.Files;
using RealEstateWebApp.UI.Components.ModalComponent.Service;
using RealEstateWebApp.UI.Components.ToastComponent.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.Extensions.Localization;
using RealEstateWebApp.UI.Resources;
using System.Net.Mime;
using RealEstateWebApp.UI.Components.LoadIndicatorLiteComponent;


namespace RealEstateWebApp.UI.Components.DropzoneComponent
{
    public partial class Dropzone : ComponentBase
    {
        [Parameter] public bool ShowFilesPreview { get; set; }
        [Parameter] public List<FileWithSourceModel> UploadedFiles {  get; set; }
        [Inject] private IToastService ToastService { get; set; }
        [Inject] private IIndicatorService IndicatorService { get; set; }
        [Inject] private IStringLocalizer<Resource> Localizer { get; set; }

        private long _maxFileSize = 1024 * 1024 * 5;
        private int _maxAllowedFiles = 12;

        protected override void OnInitialized()
        {
            UploadedFiles = new List<FileWithSourceModel>();
        }
        private string GetPhotoUrl(FileWithSourceModel file)
        {
            if (file.Id == 0)
            {
                return $"data:{file.ContentType};base64,{Convert.ToBase64String(file.FileData)}";
            }
            return file.Url;

        }
        private void RemoveItem(FileWithSourceModel file)
        {
            if (file.IsMain)
                UploadedFiles.FirstOrDefault().IsMain = true;

            if(file.Id == 0)
                UploadedFiles.Remove(file);
            else
                file.IsDeleted = true;
        }
        private async Task OnChange(InputFileChangeEventArgs e)
        {
            await IndicatorService.StartTask(async () =>
                {
                    foreach (var file in e.GetMultipleFiles(_maxAllowedFiles))
                    {
                        try
                        {
                            if (file.Size > 0)
                            {
                                var data = file.OpenReadStream(_maxFileSize);
                                byte[] fileBytes;
                                using (var ms = new MemoryStream())
                                {
                                    await data.CopyToAsync(ms);
                                    fileBytes = ms.ToArray();
                                }
                                data.Close();

                                UploadedFiles.Add(new FileWithSourceModel()
                                {
                                    FileName = file.Name,
                                    FileData = fileBytes,
                                    ContentType = file.ContentType
                                });
                            }

                        }
                        catch (Exception ex)
                        {
                            ToastService.ShowError(ex.Message);
                        }
                        if (!UploadedFiles.Exists(x => x.IsMain))
                            UploadedFiles.FirstOrDefault().IsMain = true;
                    }

                }, "loading-preview");
            
        }
        public List<FileWithSourceModel> GetUploadedPhotos()
        {
            return UploadedFiles;
        }
        public void ChangeMainPhoto(int fileId)
        {
            UploadedFiles.ForEach(x => x.IsMain = false);
            UploadedFiles.FirstOrDefault(x => x.Id == fileId).IsMain = true;
        }
    }
}
