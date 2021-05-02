using HVTApp.Infrastructure.Interfaces.Services.EventService;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.Settings.ViewModels
{
    public class UserSettingsViewModel
    {
        private readonly IUnityContainer _container;

        public DelegateLogCommand StartEventServiceCommand { get; }
        public DelegateLogCommand StopEventServiceCommand { get; }

        public UserSettingsViewModel(IUnityContainer container)
        {
            _container = container;
            
            var eventServiceClient = _container.Resolve<IEventServiceClient>();

            StartEventServiceCommand = new DelegateLogCommand(
                () =>
                {
                    eventServiceClient.Stop();
                    eventServiceClient.Start();
                });

            StopEventServiceCommand = new DelegateLogCommand(
                () =>
                {
                    eventServiceClient.Stop();
                });

        }
    }
}