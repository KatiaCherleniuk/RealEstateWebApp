using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealEstateWebApp.Business;
using RealEstateWebApp.Business.Helpers;
using RealEstateWebApp.UI.Components.LoadIndicatorLiteComponent;
using RealEstateWebApp.UI.Components.ModalComponent.Service;
using RealEstateWebApp.UI.Components.ToastComponent.Services;
using RealEstateWebApp.UI.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace RealEstateWebApp.UI.Components.Helpers
{
    public partial class EditableList<TItem> : ComponentBase where TItem : class
    {
        public class EditableListViewModel
        {
            public TItem Item { get; set; }
            public bool IsReadOnly { get; set; }
            public bool IsCreate { get; set; }
        }
        
        [Parameter] public RenderFragment Title { get; set; }
        [Parameter] public RenderFragment LeftSubTitle { get; set; }
        [Parameter] public RenderFragment RightSubTitle { get; set; }
        
        [Parameter] public RenderFragment TableHead { get; set; }
        [Parameter] public RenderFragment<TItem> TableRow { get; set; }
        [Parameter] public List<NameWithDescription> MandatoryItems { get; set; }
        [Parameter] public RenderFragment<EditableListViewModel> EditForm { get; set; }
        
        [Parameter] public IEditableListLogic<TItem> Logic { get; set; }
        
        [Parameter] public bool IsAutoLoad { get; set; } = true;
        [Parameter] public EventCallback<TItem> SelectionChanged { get; set; }
        [Parameter] public EventCallback Discard { get; set; }
        
        [Inject] private IIndicatorService IndicatorService { get; set; }
        [Inject] private IToastService ToastService { get; set; }
        [Inject] private IModalService ModalService { get; set; }
        [Inject] private ILogger<EditableList<TItem>> Logger { get; set; }
        

        private List<TItem> _items = new List<TItem>();
        private TItem _selectedItem;

        private EditableListViewModel _editedItem;

        protected override async Task OnInitializedAsync()
        {
            _editedItem = new EditableListViewModel();
            SetItems(await Logic.MakeEmptyItem(), true, false);
            if (IsAutoLoad)
                await Reload();
        }

        public async Task Reload()
        {
            _editedItem = new EditableListViewModel();
            SetItems(await Logic.MakeEmptyItem(), true, true);
            await IndicatorService.StartTask(async () =>
            {
                var items = await Logic.LoadItems();
                _items = items.ToList();
            }, "settings-editable-list");
        }

        private void SelectItem(TItem item)
        {
            if (Logic.IsEqual(item, _selectedItem))
                return;
            SetItems(item, true, false);
            StateHasChanged();
        }

        private async Task OnAddItem()
        {
            SetItems(await Logic.MakeEmptyItem(), false, true);
            StateHasChanged();
        }

        private async Task OnSave()
        {
            var isValid = await Logic.IsValidBeforeSave(_editedItem.Item, _selectedItem);
            if (!isValid)
                return;
            try
            {
                if (_editedItem.IsCreate)
                {
                    var isSuccess = await Logic.Create(_editedItem.Item);
                    if (!isSuccess)
                        return;
                    _items.Add(_editedItem.Item);
                }
                else
                {
                    var isSuccess = await Logic.Edit(_editedItem.Item);
                    if (!isSuccess)
                        return;
                    var index = _items.IndexOf(_selectedItem);
                    _items[index] = _editedItem.Item;
                }
                
                ToastService.ShowSuccess("Changes was saved successfully");
                SetItems(_editedItem.Item, true, false);
            }
            catch (Exception e)
            {
                Logger.LogError(e, e.Message);
                ToastService.ShowError("An error has occurred");
            }
        }

        private void SetItems(TItem selectedItem, bool isReadOnly, bool isCreate)
        {
            _selectedItem = selectedItem;
            _editedItem.Item = ObjectHelper.Copy(selectedItem);
            _editedItem.IsReadOnly = isReadOnly;
            _editedItem.IsCreate = isCreate;
            if (SelectionChanged.HasDelegate)
                SelectionChanged.InvokeAsync(_selectedItem);
        }

        private async Task OnDiscard()
        {
            _editedItem.IsReadOnly = true;
            _editedItem.Item = ObjectHelper.Copy(_selectedItem);
            if (Discard.HasDelegate)
                await Discard.InvokeAsync();
        }

        private void OnEdit()
        {
            _editedItem.IsReadOnly = false;
        }

        private async Task OnDelete()
        {
            var canDelete = await Logic.CanDelete((_selectedItem));
            if (!canDelete)
            {
                ToastService.ShowError("Can't delete this item");
                return; 
            }

            var confirmation = await  ModalService.Confirm("Delete confirmation", "Do you really want to delete this Item?");
            if (!confirmation)
                return;
            
            await Logic.Delete(_selectedItem);
            _items.Remove(_selectedItem);
            
            
            SetItems(await Logic.MakeEmptyItem(), true, false);
            ToastService.ShowSuccess("Item was removed successfully");
        }
    }
}