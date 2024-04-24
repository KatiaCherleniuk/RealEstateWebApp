using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;
using RealEstateWebApp.Business;
using RealEstateWebApp.Models.Files;
using RealEstateWebApp.Models.Property;
using RealEstateWebApp.Models.Record;
using RealEstateWebApp.Models.RecordValue;
using RealEstateWebApp.Models.RecordViewModels;
using RealEstateWebApp.UI.Components.CarouselComponent;
using RealEstateWebApp.UI.Components.ModalComponent.Service;
using RealEstateWebApp.UI.Components.ToastComponent.Services;
using RealEstateWebApp.UI.Resources;
using RealEstateWebApp.UI.Shared.Modals;
using System.Net;

namespace RealEstateWebApp.UI.Pages.User
{
    public partial class RecordDetails
    {
        [Parameter] public int RecordId { get; set; }
        [Inject] private IModalService ModalService { get; set; }
        [Inject] private IStringLocalizer<Resource> Localizer { get; set; }
        [Inject] private IJSRuntime JsRuntime { get; set; }
        [Inject] private IToastService ToastService { get; set; }

        [Inject] private FileService FileService { get; set; }
        [Inject] private RecordService RecordService { get; set; }
        [Inject] private PropertyService PropertyService { get; set; }

        private List<FileViewModel> _recordFiles { get; set; } = new();
        private List<PropertyWithValuesModel> _properties { get; set; } = new ();

        private RecordSimplifiedViewModel _record { get; set; } = new();
        private Carousel _carousel;
        private List<BaseFile> CarouselImage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            _record = await RecordService.GetRecordForView(RecordId);
            var files = await FileService.GetByRecordId(RecordId);
            _properties = await PropertyService.GetAllForCategory(_record.CategoryId);
            _recordFiles = files.ToList();
            CarouselImage ??= new List<BaseFile>();
            InitCarouselImage();
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender && _record?.Address != null)
            {
                await JsRuntime.InvokeVoidAsync("map.showMap", _record.Address?.Latitude, _record.Address?.Longitude, _record.Address?.DisplayName);
            }
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

        private async Task OpenModal()
        {
            var parameters = new ModalParameters();
            parameters.Add("RecordId", RecordId);

            var options = new ModalOptions();
            options.Size = ModalSize.Medium;

            var modal = ModalService.Show<LeaveCommentModal>("Leave your comment", parameters, options);
            var res = await modal.Result;
            if (!res.Cancelled && res.Data != null)
            {
                ToastService.ShowSuccess(Localizer["Success"]);
            }
        }
    }
}
