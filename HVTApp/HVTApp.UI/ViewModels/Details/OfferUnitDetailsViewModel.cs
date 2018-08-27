using System;
using HVTApp.Services.MessageService;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.UI.ViewModels
{
    public partial class OfferUnitDetailsViewModel
    {
        protected override void InitSpecialCommands()
        {
            SelectProductCommand = new DelegateCommand(SelectProductCommand_Execute1);
        }

        private void SelectProductCommand_Execute1()
        {
            Container.Resolve<IMessageService>().ShowYesNoMessageDialog("test", "test");
        }
    }
}
