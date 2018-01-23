using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;

namespace HVTApp.UI.ViewModels
{
    public interface IBaseWrapperListViewModel<TModel,TWrapper> : INotifyPropertyChanged
        where TModel : class, IBaseEntity
        where TWrapper : class, IWrapper<TModel>
    {
        IEnumerable<TWrapper> Items { get; }
        TWrapper SelectedItem { get; set; }

        ICommand NewItemCommand { get; }
        ICommand EditItemCommand { get; }
        ICommand RemoveItemCommand { get; }
        ICommand SelectItemCommand { get; }

        event EventHandler<DialogRequestCloseEventArgs> CloseRequested;
    }
}