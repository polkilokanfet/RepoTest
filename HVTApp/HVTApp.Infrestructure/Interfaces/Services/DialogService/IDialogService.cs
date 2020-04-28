using System;
using System.Windows;
using System.Windows.Controls;

namespace HVTApp.Infrastructure.Interfaces.Services.DialogService
{
    public interface IDialogService
    {
        void Register<TViewModel, TView>()
            where TViewModel : IDialogRequestClose
            where TView : IDialog;
        bool? ShowDialog<TViewModel>(TViewModel viewModel) where TViewModel : IDialogRequestClose;

        void RegisterShow<TViewModel, TView>() where TView : UserControl;
        void Show<TViewModel>(TViewModel viewModel, string title = null);
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