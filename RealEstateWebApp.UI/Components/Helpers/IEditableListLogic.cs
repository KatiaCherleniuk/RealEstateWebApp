using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateWebApp.UI.Components.Helpers
{
    public interface IEditableListLogic<TItem> where TItem : class
    {
        Task<IEnumerable<TItem>> LoadItems();
        bool IsEqual(TItem a, TItem b);
        Task<TItem> MakeEmptyItem();

        Task<bool> CanDelete(TItem item);
        Task<bool> IsValidBeforeSave(TItem newItem, TItem oldItem);

        Task<bool> Create(TItem item);
        Task<bool> Edit(TItem item);
        Task<bool> Delete(TItem item);
    }
}