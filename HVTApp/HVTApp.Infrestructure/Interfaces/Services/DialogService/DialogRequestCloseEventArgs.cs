using System;

namespace HVTApp.Infrastructure.Interfaces.Services.DialogService
{
    public class DialogRequestCloseEventArgs : EventArgs
    {
        public bool? DialogResult { get; }

        public DialogRequestCloseEventArgs(bool? dialogResult)
        {
            DialogResult = dialogResult;
        }
    }
}