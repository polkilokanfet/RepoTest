using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;

namespace HVTApp.Infrastructure.Interfaces.Services.SelectService
{
    public interface ISelectService
    {
        void Register<TViewModel, TView, TItem>() 
            where TViewModel : ISelectViewModel<TItem>
            where TView : Control;
        TItem SelectItem<TItem>(IEnumerable<TItem> items, TItem selectedItem = null) 
            where TItem : class ;
    }

    public interface ISelectViewModel<TItem> : IDialogRequestClose
    {
        ICollection<TItem> Items { get; }
        TItem SelectedItem { get; set; }
        ICommand SelectItemCommand { get; }
        ICommand NewItemCommand { get; }
    }
}