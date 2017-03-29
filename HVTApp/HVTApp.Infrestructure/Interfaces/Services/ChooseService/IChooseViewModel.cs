using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace HVTApp.Infrastructure.Interfaces.Services.ChooseService
{
    public interface IChooseViewModel<TItem> : IChooseRequest<TItem>, INotifyPropertyChanged
    {
        ICollectionView Items { get; }
        TItem SelectedItem { get; set; }
        ICommand ChooseCommand { get; }

        string FilterString { get; set; }
    }

    public interface IChooseRequest<TItem>
    {
        event EventHandler<ChooseDialogEventArgs<TItem>> ChooseRequested;
    }
}
