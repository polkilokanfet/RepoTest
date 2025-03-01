using System;
using System.Windows;

namespace HVTApp.Infrastructure.Interfaces.Services.DialogService
{
    public interface IDialogService
    {
        void Register<TViewModel, TView>()
            where TView : IDataContext;
        bool? ShowDialog<TViewModel>(TViewModel viewModel, string title = null) 
            where TViewModel : IDialogRequestClose;

        void Show<TViewModel>(TViewModel viewModel, string title = null);
    }

    public interface IDataContext
    {
        object DataContext { get; set; }
    }

    public interface IDialog
    {
        object DataContext { get; set; }
        bool? DialogResult { get; set; }
        Window Owner { get; set; }
        void Close();
        bool? ShowDialog();
    }

    public interface IDialogRequestClose
    {
        event EventHandler<DialogRequestCloseEventArgs> CloseRequested;
    }
}