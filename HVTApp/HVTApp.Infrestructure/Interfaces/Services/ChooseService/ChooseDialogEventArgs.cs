using System;

namespace HVTApp.Infrastructure.Interfaces.Services.ChooseService
{
    public class ChooseDialogEventArgs<TChoosenItem> : EventArgs
    {
        public TChoosenItem ChoosenItem { get; }

        public ChooseDialogEventArgs(TChoosenItem choosenItem)
        {
            ChoosenItem = choosenItem;
        }
    }
}
