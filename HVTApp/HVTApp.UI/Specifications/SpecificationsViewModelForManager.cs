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
using HVTApp.UI.Modules.Sales.Market;
using HVTApp.UI.Modules.Sales.Views;
using Microsoft.Practices.Unity;
using Prism.Regions;

namespace HVTApp.UI.Specifications
{
    public class SpecificationsViewModelForManager : SpecificationsViewModelBase
    {
        public SpecificationsViewModelForManager(IUnityContainer container, Market2ViewModel market2ViewModel) : base(container)
        {
            market2ViewModel.SelectedProjectItemChanged += item =>
            {
                var specifications = item == null
                    ? new List<Specification>()
                    : item.SalesUnits
                        .Select(salesUnit => salesUnit.Specification)
                        .Where(specification => specification != null)
                        .Distinct();
                this.Load(specifications);
            };
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
    }
}