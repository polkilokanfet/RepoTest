using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using HVTApp.UI.Views;
using Prism.Regions;
using Microsoft.Practices.Unity;


namespace HVTApp.UI.ViewModels
{
    public partial class DesignDepartmentLookupListViewModel
    {
        protected override void InitSpecialCommands()
        {
            base.InitSpecialCommands();

            this.NewItemCommand = new DelegateLogCommand(
                () =>
                {
                    Container.Resolve<IRegionManager>().RequestNavigateContentRegion<DesignDepartmentView>(new NavigationParameters());
                });

            this.EditItemCommand = new DelegateLogCommand(
                () =>
                {
                    Container.Resolve<IRegionManager>().RequestNavigateContentRegion<DesignDepartmentView>(new NavigationParameters(){{nameof(DesignDepartment), SelectedLookup.Entity}});
                },
                () => this.SelectedLookup != null);
        }
    }
}