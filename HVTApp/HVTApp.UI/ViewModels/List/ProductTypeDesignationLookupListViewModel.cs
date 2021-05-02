using HVTApp.Infrastructure.Extansions;
using HVTApp.UI.Commands;
using HVTApp.UI.Modules.Products.Views;
using Prism.Regions;

namespace HVTApp.UI.ViewModels
{
    public partial class ProductTypeDesignationLookupListViewModel
    {
        protected override void InitSpecialCommands()
        {
            NewItemCommand = new DelegateLogCommand(() =>
            {
                RegionManager.RequestNavigateContentRegion<ProductTypeDesignationView>(new NavigationParameters());
            });
        }
    }
}