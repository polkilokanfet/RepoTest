using System.Windows.Input;
using HVTApp.Infrastructure.Interfaces.Services.EventService;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.UI.Modules.Settings.ViewModels
{
    public class UserSettingsViewModel
    {
        private readonly IUnityContainer _container;

        public ICommand StartEventServiceCommand { get; }
        public ICommand StopEventServiceCommand { get; }

        public UserSettingsViewModel(IUnityContainer container)
        {
            _container = container;
            
            var eventServiceClient = _container.Resolve<IEventServiceClient>();

            StartEventServiceCommand = new DelegateCommand(
                () =>
                {
                    eventServiceClient.Stop();
                    eventServiceClient.Start();
                });

            StopEventServiceCommand = new DelegateCommand(
                () =>
                {
                    eventServiceClient.Stop();
                });

        }
    }
}