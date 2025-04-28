using System;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
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
        public DelegateLogCommand CopyStaffCommand { get; private set; }

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

            CopyStaffCommand = new DelegateLogCommand(
                () =>
                {
                    var dep = Container.Resolve<ISelectService>().SelectItem(this.SelectedItems);
                    if (dep == null) return;
                    using (var unitOfWork = Container.Resolve<IUnitOfWork>())
                    {
                        var staff = dep.Staff.Select(user => unitOfWork.Repository<User>().GetById(user.Id)).ToList();
                        var observers = dep.Observers.Select(user => unitOfWork.Repository<User>().GetById(user.Id)).ToList();
                        foreach (var department in SelectedItems.Select(department => unitOfWork.Repository<DesignDepartment>().GetById(department.Id)))
                        {
                            department.Staff.Clear();
                            department.Staff.AddRange(staff);

                            department.Observers.Clear();
                            department.Observers.AddRange(observers);
                        }

                        unitOfWork.SaveChanges();
                    }
                },
                () => SelectedItems != null && SelectedItems.Any());

            this.SelectedLookupChanged += lookup =>
            {
                CopyDesignDepartmentCommand.RaiseCanExecuteChanged();
                CopyStaffCommand.RaiseCanExecuteChanged();
            };
        }
    }
}