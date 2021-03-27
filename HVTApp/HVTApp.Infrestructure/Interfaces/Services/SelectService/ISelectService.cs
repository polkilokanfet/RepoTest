using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;

namespace HVTApp.Infrastructure.Interfaces.Services.SelectService
{
    public interface ISelectService
    {
        void Register<TView, TItem>() 
            where TView : Control 
            where TItem : class, IBaseEntity;

        TItem SelectItem<TItem>(IEnumerable<TItem> items, Guid? selectedItemId = null)
            where TItem : class, IBaseEntity;

        IEnumerable<TItem> SelectItems<TItem>(IEnumerable<TItem> items)
            where TItem : class, IBaseEntity;
    }

    public interface ISelectServiceViewModel<TItem> : IDialogRequestClose
        where TItem : IBaseEntity
    {
        void Load(IEnumerable<TItem> entities);

        TItem SelectedItem { get; set; }
        ICommand SelectItemCommand { get; }

        IEnumerable<TItem> SelectedItems { get; }
        ICommand SelectItemsCommand { get; }


        ICommand NewItemCommand { get; }
    }
}