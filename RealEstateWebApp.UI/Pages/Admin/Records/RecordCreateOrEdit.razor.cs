using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;
using RealEstateWebApp.Business;
using RealEstateWebApp.Models.Address;
using RealEstateWebApp.Models.Files;
using RealEstateWebApp.Models.Property;
using RealEstateWebApp.Models.RecordValue;
using RealEstateWebApp.Models.RecordViewModels;
using RealEstateWebApp.Models;
using RealEstateWebApp.UI.Components.DropzoneComponent;
using RealEstateWebApp.UI.Components.LoadIndicatorLiteComponent;
using RealEstateWebApp.UI.Components.ModalComponent.Service;
using RealEstateWebApp.UI.Components.ToastComponent.Services;
using RealEstateWebApp.UI.Resources;
using RealEstateWebApp.Business.Helpers;
using RealEstateWebApp.UI.Services;
using RealEstateWebApp.UI.Shared.Modals;

namespace RealEstateWebApp.UI.Pages.Admin.Records
{
    public partial class RecordCreateOrEdit: ComponentBase
    {
        [Parameter] public int CategoryId { get; set; }
        [Parameter] public int RecordId { get; set; }

        [Inject] private CategoryService CategoryService { get; set; }
        [Inject] private PropertyService PropertyService { get; set; }
        [Inject] private RecordService RecordService { get; set; }
        [Inject] private FileService FileService { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }
        [Inject] private IModalService ModalService { get; set; }
        [Inject] private IStringLocalizer<Resource> Localizer { get; set; }
        [Inject] private IIndicatorService IndicatorService { get; set; }
        [Inject] private IToastService ToastService { get; set; }
        [Inject] private IJSRuntime JsRuntime { get; set; }
        [Inject] private ILogger<RecordCreateOrEdit> Logger { get; set; }

        private IEnumerable<TitleAndIdModel> _categories;
        private IEnumerable<PropertyWithValuesModel> _properties;
        private List<FileWithSourceModel> _recordsFiles;
        private RecordEditViewModel _originalRecordModel;
        private RecordEditViewModel _workingRecordModel;
        private Dropzone _dropzoneComponent;

        protected override void OnInitialized()
        {
            _categories = new List<TitleAndIdModel>();
            _properties = new List<PropertyWithValuesModel>();
            _recordsFiles = new List<FileWithSourceModel>();
            _workingRecordModel = RecordService.MakeNewRecordForCreate(CategoryId);
        }

        protected override async Task OnParametersSetAsync()
        {
            _categories = await CategoryService.GetAllTitleOnly();

            if (RecordId == 0)
                _originalRecordModel = RecordService.MakeNewRecordForCreate(CategoryId);
            else
            {
                _originalRecordModel = await RecordService.GetRecordForEdit(RecordId);
                await GetRecordsFiles();
            }

            _workingRecordModel = ObjectHelper.Copy(_originalRecordModel);

            await CategoryChanged();
        }
        private async Task OpenMapModal(int propertyIdForAddress)
        {
            var parameters = new ModalParameters();
            parameters.Add("RecordId", RecordId);
            parameters.Add("Address", _workingRecordModel.Address);

            var options = new ModalOptions();
            options.Size = ModalSize.XLarge;

            var modal = ModalService.Show<AddressModal>("Select address", parameters, options);
            var res = await modal.Result;
            if (!res.Cancelled && res.Data != null)
            {
                var address = (AddressModel)res.Data;
                _workingRecordModel.Address = address;
            }
        }
        private async Task GetRecordsFiles()
        {
            var files = await FileService.GetByRecordId(RecordId);
            if (files == null || !files.Any())
                return;
            foreach (var file in files)
                _recordsFiles.Add(new FileWithSourceModel
                {
                    Id = file.Id,
                    Url = $"file/GetByIdAndRecordId?recordId={RecordId}&fileId={file.Id}",
                    FileName = file.FileName,
                    IsDeleted = file.IsDeleted
                });
        }

        private async Task CategoryChanged()
        {
            await IndicatorService.StartTask(async () =>
            {
                _properties = await PropertyService.GetAllForCategory(_workingRecordModel.CategoryId);
            }, "record-edit-all");
        }

        private RecordPropertyValueBasicModel GetValue(PropertyWithValuesModel property)
        {
            var value = _workingRecordModel.Values.FirstOrDefault(v => v.PropertyId == property.Id);
            if (value == null)
            {
                value = new RecordPropertyValueBasicModel()
                {
                    RecordId = RecordId,
                    PropertyId = property.Id
                };
                _workingRecordModel.Values.Add(value);
            }

            return value;
        }

        private async Task Save()
        {
            try
            {
                await IndicatorService.StartTask(async () =>
                {
                    if (_workingRecordModel.Id == 0)
                        await RecordService.Create(_workingRecordModel);
                    else
                        await RecordService.Update(_workingRecordModel);

                    await UploadFiles();

                    _originalRecordModel = ObjectHelper.Copy(_workingRecordModel);

                }, "record-edit-all");

                ToastService.ShowSuccess("Changes was saved successfully");
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                ToastService.ShowError("An error has occurred");
            }
        }

        private async Task UploadFiles()
        {
            var files = _dropzoneComponent.UploadedFiles;
            foreach (var item in files)
            {
                var preparedFile = new RecordsFileModel()
                {
                    Id = item.Id,
                    FileData = item.FileData,
                    FileName = item.FileName,
                    ContentType = item.ContentType,
                    RecordId = _workingRecordModel.Id,
                    CategoryId = _workingRecordModel.CategoryId,
                    UserId = sessionUserResolver.User.Id,
                    IsDeleted = item.IsDeleted
                };
                await FileService.SaveFile(preparedFile);
            }
        }

        private async Task DeleteRecord()
        {
            var resultConfirmed = await ModalService.Confirm(Localizer["Confirm delete"], Localizer["Do you really want to delete this record?"] + "\n" + Localizer["It will be permanent operation"]);
            if (!resultConfirmed)
                return;

            var deleteResult = await RecordService.DeleteRecord(_workingRecordModel.Id, _workingRecordModel.CategoryId);
            if (deleteResult)
            {
                ToastService.ShowSuccess(Localizer["Record was deleted successfully"]);
                NavigationManager.NavigateTo($"/category/{_workingRecordModel.CategoryId}");
            }
            else
                ToastService.ShowError(Localizer["Delete record failed"]);
        }

        private void Discard()
        {
            _workingRecordModel = ObjectHelper.Copy(_originalRecordModel);
        }
    }
}

