using Microsoft.AspNetCore.Components;
using RealEstateWebApp.Business;
using RealEstateWebApp.Models.Files;
using RealEstateWebApp.UI.Components.CarouselComponent;

namespace RealEstateWebApp.UI.Pages.User
{
    public partial class RecordDetails
    {
        [Parameter] public int RecordId { get; set; }

        [Inject] private FileService FileService { get; set; }
        private List<FileViewModel> _recordFiles;
        private Carousel _carousel;
        private List<BaseFile> CarouselImage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var files = await FileService.GetByRecordId(RecordId);
            _recordFiles = files.ToList();
            CarouselImage ??= new List<BaseFile>();
            InitCarouselImage();
        }

        private void OnFileClick(object item)
        {
            if (item is not FileViewModel recordFile)
                return;
            InitCarouselImage(recordFile);
        }

        private void InitCarouselImage(FileViewModel recordFile = null)
        {
            if (_recordFiles == null || !_recordFiles.Any())
                return;
            foreach (var file in _recordFiles)
                CarouselImage.Add(new BaseFile
                {
                    Url = $"file/GetByIdAndRecordId?recordId={RecordId}&fileId={file.Id}",
                    IsSelected = recordFile != null && file.Id == recordFile.Id,
                });
        }

        private void OnCarouselItemClicked(object sender, int index)
        {
            _carousel.ShowFullFiled(index);
        }
    }
}
