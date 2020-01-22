using System.Windows.Input;
using HVTApp.Infrastructure;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.UI.Modules.Settings.ViewModels
{
    public class AdminViewModel
    {
        public ICommand Command { get; }

        public AdminViewModel(IUnityContainer container)
        {
            var unitOfWork = container.Resolve<IUnitOfWork>();
            Command = new DelegateCommand(
                () =>
                {
                    
                });
        }
    }
}