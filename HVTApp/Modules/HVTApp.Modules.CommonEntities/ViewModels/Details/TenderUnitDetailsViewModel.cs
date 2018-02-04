using System;
using System.Windows.Input;
using Prism.Commands;

namespace HVTApp.UI.ViewModels
{
    public partial class TenderUnitDetailsViewModel
    {
        public ICommand SelectTenderCommand { get; private set; }

        protected override void InitCommands()
        {
            SelectTenderCommand = new DelegateCommand(SelectTender_Execute);
        }

        private void SelectTender_Execute()
        {
            throw new NotImplementedException();
        }
    }
}
