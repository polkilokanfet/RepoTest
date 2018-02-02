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
    public interface IBaseListViewModel<TEntity, TLookup> : INotifyPropertyChanged
        where TEntity : class, IBaseEntity
        where TLookup : class, ILookupItemNavigation<TEntity>
    {
        Task LoadAsync();
        Task InjectItems(IEnumerable<TEntity> entities);

        IEnumerable<TLookup> Lookups { get; }
        TLookup SelectedLookup { get; set; }

        ICommand NewItemCommand { get; }
        ICommand EditItemCommand { get; }
        ICommand RemoveItemCommand { get; }
        ICommand SelectItemCommand { get; }

        event EventHandler<DialogRequestCloseEventArgs> CloseRequested;

    }
}