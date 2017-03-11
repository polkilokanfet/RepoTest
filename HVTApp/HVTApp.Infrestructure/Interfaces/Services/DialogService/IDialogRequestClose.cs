using System;

namespace HVTApp.Infrastructure.Interfaces.Services.DialogService
{
    public interface IDialogRequestClose
    {
        event EventHandler<DialogRequestCloseEventArgs> CloseRequested;
    }
}