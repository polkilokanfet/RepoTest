using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using HVTApp.DataAccess;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using HVTApp.UI.Lookup;
using HVTApp.UI.Modules.Sales.Views;
using HVTApp.UI.ViewModels;
using Microsoft.Practices.Unity;
using Prism.Regions;

namespace HVTApp.UI.Specifications
{
    public class SpecificationsViewModelForManager : SpecificationLookupListViewModel
    {
        public SpecificationsViewModelForManager(IUnityContainer container) : base(container)
        {
        }

        public override IEnumerable<SpecificationLookup> GetAllLookups()
        {
            return ((ISalesUnitRepository) UnitOfWork.Repository<SalesUnit>())
                .GetAllOfCurrentUser()
                .Where(x => x.Specification != null)
                .Select(x => x.Specification)
                .Distinct()
                .OrderByDescending(x => x.Date)
                .ThenBy(x => x.Contract.Number)
                .ThenBy(x => x.Number)
                .Select(x => new SpecificationLookup(x));
        }

        protected override void InitSpecialCommands()
        {
            EditItemCommand = new DelegateLogCommand(
                () =>
                {
                    RegionManager.RequestNavigateContentRegion<SpecificationView>(new NavigationParameters {{nameof(Specification), SelectedItem}});
                },
                () => SelectedItem != null);

            RemoveItemCommand = new DelegateLogCommand(
                () =>
                {
                    var dr = MessageService.ConfirmationDialog("Удаление", $"Вы действительно хотите удалить \"{SelectedLookup.DisplayMember}\"?", defaultNo: true);
                    if (dr == false) return;

                    var unitOfWork = Container.Resolve<IUnitOfWork>();

                    var specification = unitOfWork.Repository<Specification>().GetById(SelectedLookup.Id);
                    if (specification != null)
                    {
                        var salesUnits = unitOfWork.Repository<SalesUnit>().Find(x => x.Specification?.Id == specification.Id);
                        salesUnits.ForEach(salesUnit => salesUnit.Specification = null);
                        try
                        {
                            unitOfWork.Repository<Specification>().Delete(specification);
                            unitOfWork.SaveChanges();
                        }
                        catch (DbUpdateException e)
                        {
                            MessageService.Message(e.GetType().ToString(), e.PrintAllExceptions());
                            return;
                        }
                    }

                    EventAggregator.GetEvent<AfterRemoveSpecificationEvent>().Publish(specification);

                },
                () => SelectedItem != null);
        }

        private void EditItemCommandExecute()
        {
            RegionManager.RequestNavigateContentRegion<SpecificationView>(new NavigationParameters { { nameof(Specification), SelectedItem } });
        }
    }

    public class SpecificationsViewModelForBackManager : SpecificationLookupListViewModel
    {
        public SpecificationsViewModelForBackManager(IUnityContainer container) : base(container)
        {
        }

        protected override void InitSpecialCommands()
        {
            EditItemCommand = null;
        }
    }
}