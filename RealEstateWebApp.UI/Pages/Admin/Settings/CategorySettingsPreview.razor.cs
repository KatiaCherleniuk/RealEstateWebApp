using Microsoft.AspNetCore.Components;
using RealEstateWebApp.Business.Helpers;
using RealEstateWebApp.Business;
using RealEstateWebApp.Models;
using RealEstateWebApp.Models.Property;
using RealEstateWebApp.UI.Components.Helpers;
using RealEstateWebApp.UI.Shared.Navbar;
using RealEstateWebApp.UI.Components.ToastComponent.Services;
using Microsoft.Extensions.Localization;
using RealEstateWebApp.Models.Category;
using RealEstateWebApp.UI.Resources;
using System.Threading.Tasks;

namespace RealEstateWebApp.UI.Pages.Admin.Settings
{
    public partial class CategorySettingsPreview : ComponentBase, IEditableListLogic<PropertyWithValuesCountModel>
    {
        [Parameter] public int CategoryId { get; set; }

        [Inject] private NavigationManager NavigationManager { get; set; }
        [Inject] public IStringLocalizer<Resource> Localizer { get; set; }
        [Inject] private IToastService ToastService { get; set; }
        [Inject] private CategoryService CategoryService { get; set; }
        [Inject] private PropertyService PropertyService { get; set; }
        [Inject] private PropertyValueService PropertyValueService { get; set; }
        public List<NameWithDescription> MandatoryItems { get; set; }

        private CategoryEditModel _categoryModel = new();
        private EditableList<PropertyWithValuesCountModel> _editableList;
        private List<TitleAndIdModel> _values = new List<TitleAndIdModel>();
        private IEnumerable<TitleAndIdModel> _originalValues;
        private List<Func<Task>> tasksSavingProperties = new List<Func<Task>>();

        protected override async Task OnInitializedAsync()
        {
            MandatoryItems = new List<NameWithDescription>
            {
               new NameWithDescription(name: Localizer["Price"], description: $"{Localizer["Price"]}{Localizer["is mandatory field. You should add it to each record"]}"),
               new NameWithDescription(name: Localizer["Square"], description: $"{Localizer["Square"]}{Localizer["is mandatory field. You should add it to each record"]}" ),
               new NameWithDescription(name: Localizer["Address"], description: $"{Localizer["Address"]}{Localizer["is mandatory field. You should add it to each record"]}")
            };

            if (_editableList != null)
                await _editableList.Reload();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
                await _editableList.Reload();
        }

        public async Task<IEnumerable<PropertyWithValuesCountModel>> LoadItems()
        {
            if(CategoryId == 0)
                return new List<PropertyWithValuesCountModel>();

            var categoryTask = CategoryService.GetOneTitleOnly(CategoryId);
            var propertiesTask = PropertyService.GetAllWithValuesCount(CategoryId);

            await Task.WhenAll(categoryTask, propertiesTask);

            _categoryModel = categoryTask.Result;
            StateHasChanged();

            return propertiesTask.Result;
        }

        public bool IsEqual(PropertyWithValuesCountModel a, PropertyWithValuesCountModel b)
        {
            if (a == null || b == null)
                return false;
            return a.Id == b.Id;
        }

        public Task<PropertyWithValuesCountModel> MakeEmptyItem()
        {
            var res = new PropertyWithValuesCountModel()
            {
                CategoryId = CategoryId
            };
            return Task.FromResult(res);
        }

        public Task<bool> CanDelete(PropertyWithValuesCountModel item)
        {
            var res = item.RecordsCount <= 0;
            return Task.FromResult(res);
        }

        public Task<bool> IsValidBeforeSave(PropertyWithValuesCountModel newItem, PropertyWithValuesCountModel oldItem)
        {
            return Task.FromResult(true);
        }

        public Task<bool> Create(PropertyWithValuesCountModel item)
        {
            return Save(item, PropertyService.Create);
        }

        public Task<bool> Edit(PropertyWithValuesCountModel item)
        {
            return Save(item, PropertyService.Update);
        }

        private async Task<bool> Save(PropertyWithValuesCountModel item, Func<PropertyWithValuesCountModel, IEnumerable<TitleAndIdModel>, Task> saveFnc)
        {
            var values = _values.Where(v => !string.IsNullOrWhiteSpace(v.Title));
            tasksSavingProperties.Add(() => saveFnc(item, values));
            //await OnSelectionChanged(item);
            return true;
        }

        public Task<bool> Delete(PropertyWithValuesCountModel item)
        {
            return PropertyService.Delete(item.Id);
        }

        private bool IsListType(PropertyWithValuesCountModel item)
        {
            return item.Type is (PropertyType.ListSingle or PropertyType.ListMultiple);
        }

        private void OnTypeChanged(PropertyWithValuesCountModel item)
        {
            if (!IsListType(item))
            {
                _originalValues = new List<TitleAndIdModel>();
                _values = _originalValues.Select(ObjectHelper.Copy).ToList();
            }
        }

        private void AddValue()
        {
            _values.Add(new TitleAndIdModel() { Id = 0 });
        }

        private async Task OnSelectionChanged(PropertyWithValuesCountModel item)
        {
            /*_values.Add()
            if (item.Id == 0)
                _originalValues = new List<TitleAndIdModel>();
            else
                _originalValues = await PropertyValueService.GetAllTitleOnly(item.Id);
            _values = _originalValues.ToDictionary(o => ObjectHelper.Copy(o), _ => false);*/
        }
        private void DeleteValue(TitleAndIdModel value)
        {
            _values.Remove(value);
        }

        private Task Discard()
        {
            _values = _originalValues.Select(ObjectHelper.Copy).ToList();
            return Task.CompletedTask;
        }

        private async Task SaveCategory()
        {
            if (string.IsNullOrWhiteSpace(_categoryModel.Title))
                return; 

            var res = _categoryModel.Id == 0 ? await CategoryService.Create(_categoryModel) : await CategoryService.Update(_categoryModel);
            await Task.WhenAll(tasksSavingProperties.Select(taskFunc => taskFunc()));

            if (res)
            {
                ToastService.ShowSuccess($"{Localizer["Category"]} {Localizer["was saved successfully"]}!");
                return;
            }
            ToastService.ShowSuccess($"{Localizer["smth went wrong"]}!");
        }

        private async Task DeleteCategory()
        {
            var res = await CategoryService.Remove(CategoryId);
            if(res)
            {
                NavigationManager.NavigateTo("/admin/dashboard");
                ToastService.ShowSuccess($"{Localizer["Category"]} \"{_categoryModel.Title}\" {Localizer["was delete successfully"]}!");
                return;
            }
            ToastService.ShowSuccess($"{Localizer["smth went wrong"]}!");

        }
    }
}
