using HVTApp.Infrastructure.Extansions;
using HVTApp.UI.Modules.Products.Views;
using Prism.Commands;
using Prism.Regions;

namespace HVTApp.UI.ViewModels
{
    public partial class ProductTypeDesignationLookupListViewModel
    {
        protected override void InitSpecialCommands()
        {
            NewItemCommand = new DelegateCommand(() =>
            {
                RegionManager.RequestNavigateContentRegion<ProductTypeDesignationView>(new NavigationParameters());
            });
        }
    }
}