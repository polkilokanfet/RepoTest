using System;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using HVTApp.UI.Views;
using Prism.Regions;
using Microsoft.Practices.Unity;


namespace HVTApp.UI.ViewModels
{
    public partial class DesignDepartmentLookupListViewModel
    {
        public DelegateLogCommand CopyDesignDepartmentCommand { get; private set; }

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
                    Container.Resolve<IRegionManager>().RequestNavigateContentRegion<DesignDepartmentView>(new NavigationParameters()
                    {
                        {nameof(DesignDepartment), SelectedLookup.Entity}
                    });
                },
                () => this.SelectedLookup != null);


            CopyDesignDepartmentCommand = new DelegateLogCommand(
                () =>
                {
                    Container.Resolve<IRegionManager>().RequestNavigateContentRegion<DesignDepartmentView>(new NavigationParameters()
                    {
                        { nameof(DesignDepartment), SelectedLookup.Entity },
                        { string.Empty, SelectedLookup.Entity }
                    });
                },
                () => this.SelectedLookup != null);

            this.SelectedLookupChanged += lookup => CopyDesignDepartmentCommand.RaiseCanExecuteChanged();
        }
    }
}