using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.UI.Lookup;

namespace HVTApp.UI.ViewModels
{
    public interface IBaseListViewModel<TLookupItem> : INotifyPropertyChanged
        where TLookupItem : ILookupItem
    {
        ICollection<TLookupItem> Items { get; }
        TLookupItem SelectedItem { get; set; }

        ICommand NewItemCommand { get; }
        ICommand EditItemCommand { get; }
        ICommand RemoveItemCommand { get; }
        ICommand SelectItemCommand { get; }

        event EventHandler<DialogRequestCloseEventArgs> CloseRequested;
    }
}