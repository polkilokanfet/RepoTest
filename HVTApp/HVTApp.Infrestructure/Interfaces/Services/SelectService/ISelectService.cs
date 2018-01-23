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
            where TViewModel : ISelectViewModel<TItem>
            where TView : Control 
            where TItem : IWrapper<IBaseEntity>;

        TItem SelectItem<TItem>(IEnumerable<TItem> items, Guid selectedItemId = default(Guid)) 
            where TItem : class, IWrapper<IBaseEntity>;
    }

    public interface ISelectViewModel<TItem> : IDialogRequestClose
        where TItem : IWrapper<IBaseEntity>
    {
        IEnumerable<TItem> Items { get; }
        TItem SelectedItem { get; set; }
        ICommand SelectItemCommand { get; }
        ICommand NewItemCommand { get; }
    }
}