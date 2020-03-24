using HVTApp.Infrastructure;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.BookRegistration.ViewModels
{
    public class IncomingRequestsViewModel : ViewModelBase
    {
        public IncomingRequestsViewModel(IUnityContainer container) : base(container)
        {
        }
    }
}