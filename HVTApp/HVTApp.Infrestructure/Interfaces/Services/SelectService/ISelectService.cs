using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;

namespace HVTApp.Infrastructure.Interfaces.Services.SelectService
{
    public interface ISelectService
    {
        void Register<TViewModel, TView, TItem>() 
            where TViewModel : ISelectServiceViewModel<TItem>
            where TView : Control 
            where TItem : class, IBaseEntity;

        TItem SelectItem<TItem>(IEnumerable<TItem> items, Guid selectedItemId = default(Guid)) 
            where TItem : class, IBaseEntity;
    }

    public interface ISelectServiceViewModel<TItem> : IDialogRequestClose
        where TItem : IBaseEntity
    {
        //TItem SelectedItem { get; set; }
        ICommand SelectItemCommand { get; }
        ICommand NewItemCommand { get; }
    }
}