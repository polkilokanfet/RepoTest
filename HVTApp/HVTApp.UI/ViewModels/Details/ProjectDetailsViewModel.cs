using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Converter;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.ViewModels
{
    public partial class ProjectDetailsViewModel : WrapperWithUnitsViewModel<ProjectWrapper, Project, SalesUnit, SalesUnitWrapper, AfterSaveProjectEvent>
    {
        protected override async void AddCommand_Execute()
        {
            var unit = await Container.Resolve<IUpdateDetailsService>().UpdateDetailsWithoutSaving(new SalesUnit { Project = Item.Model });
            if (unit == null) return;

            unit.Product = await WrapperDataService.GetRepository<Product>().GetByIdAsync(unit.Product.Id);
            unit.Facility = await WrapperDataService.GetRepository<Facility>().GetByIdAsync(unit.Facility.Id);
            unit.PaymentConditionSet = await WrapperDataService.GetRepository<PaymentConditionSet>().GetByIdAsync(unit.PaymentConditionSet.Id);

            var wrapper = new SalesUnitWrapper(unit);
            Groups.Add(new UnitsGroup(new[] { wrapper }));
            Item.Units.Add(wrapper);
            WrapperDataService.GetRepository<SalesUnit>().Add(unit);
        }
    }
}