function InitSort(dotNetObject)
{
    $('.properties-settings > div').sortable({
        axis:'y',
        placeholder: "properties-settings-sort-placeholder",
        start: (e, ui) => {
            const oldIndex = ui.item.index();
            ui.item.data('oldIndex', oldIndex);
        },
        update: (e, ui) => {
            const newIndex = ui.item.index();
            const oldIndex = ui.item.data('oldIndex');
            console.log(e, ui, oldIndex, newIndex);
            dotNetObject.invokeMethodAsync("ReorderProperty", oldIndex, newIndex);
        }
    });
}