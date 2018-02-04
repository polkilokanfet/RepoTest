using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;

namespace HVTApp.Infrastructure.Interfaces.Services.SelectService
{
    public interface ISelectService
    {
        void Register<TView, TLookup>() 
            where TView : Control 
            where TLookup : class, ILookupItem;

        TLookup SelectItem<TLookup>(IEnumerable<TLookup> items, Guid? selectedItemId = null) 
            where TLookup : class, ILookupItem;
    }

    public interface ISelectServiceViewModel<TItem> : IDialogRequestClose
        //where TItem : IBaseEntity
    {
        Task LoadAsync(IEnumerable<TItem> entities);

        TItem SelectedLookup { get; set; }
        ICommand SelectItemCommand { get; }
        ICommand NewItemCommand { get; }
    }
}