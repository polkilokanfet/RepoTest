using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using System.Threading.Tasks;
using HVTApp.UI.Lookup;

namespace HVTApp.UI.ViewModels
{
    public interface IBaseListViewModel<TLookup> : INotifyPropertyChanged
        where TLookup : ILookupItem
    {
        Task LoadAsync();

        IEnumerable<TLookup> Lookups { get; }
        TLookup SelectedLookup { get; set; }

        ICommand NewItemCommand { get; }
        ICommand EditItemCommand { get; }
        ICommand RemoveItemCommand { get; }
        ICommand SelectItemCommand { get; }

        event EventHandler<DialogRequestCloseEventArgs> CloseRequested;
    }
}