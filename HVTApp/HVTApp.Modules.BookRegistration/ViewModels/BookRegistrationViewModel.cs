using HVTApp.UI.ViewModels;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.BookRegistration.ViewModels
{
    public class BookRegistrationViewModel : DocumentLookupListViewModel
    {
        public BookRegistrationViewModel(IUnityContainer container) : base(container)
        {
        }
    }
}
