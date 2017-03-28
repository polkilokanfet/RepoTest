using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace HVTApp.Infrastructure.Interfaces.Services.ChooseService
{
    public interface IChooseViewModel<TChoosenItem> : IChooseRequest<TChoosenItem>, INotifyPropertyChanged
    {
        IEnumerable<TChoosenItem> Items { get; }
        TChoosenItem SelectedItem { get; set; }
        ICommand ChooseCommand { get; }
    }

    public interface IChooseRequest<TChoosenItem>
    {
        event EventHandler<ChooseDialogEventArgs<TChoosenItem>> ChooseRequested;
    }
}
