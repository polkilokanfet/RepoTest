using HVTApp.UI.ViewModels;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class MarketProjectListViewModel : ProjectListViewModel
    {
        public MarketProjectListViewModel(IUnityContainer container) : base(container)
        {
        }
    }
}