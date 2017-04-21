using System.Collections.Generic;
using System.Windows.Input;

namespace HVTApp.Infrastructure.Interfaces.Services.SelectService
{
    public interface ISelectService
    {
        void Register<TViewModel, TView, TItem>() where TViewModel : ISelectViewModel<TItem>;
        TItem SelectItem<TItem>(IEnumerable<TItem> items, TItem selectedItem);
    }

    public interface ISelectViewModel<TItem>
    {
        TItem SelectedItem { get; }
        ICommand NewItemCommand { get; }
    }
}