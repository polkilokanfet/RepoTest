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
        void Register<TViewModel, TView, TItem>() 
            where TViewModel : ISelectServiceViewModel<TItem>
            where TView : Control 
            where TItem : class, ILookupItem;

        TItem SelectItem<TItem>(IEnumerable<TItem> items, Guid selectedItemId = default(Guid)) 
            where TItem : class, ILookupItem;
    }

    public interface ISelectServiceViewModel<TItem> : IDialogRequestClose
        //where TItem : IBaseEntity
    {
        Task InjectItems(IEnumerable<TItem> entities);

        TItem SelectedLookup { get; set; }
        ICommand SelectItemCommand { get; }
        ICommand NewItemCommand { get; }
    }
}