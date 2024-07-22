using System.Collections.Generic;
using System.Linq;
using HVTApp.DataAccess;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using HVTApp.UI.Lookup;
using HVTApp.UI.Modules.Sales.Views;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.ViewModels
{
    public partial class SpecificationLookupListViewModel
    {
        protected override void InitSpecialCommands()
        {
            EditItemCommand = new DelegateLogCommand(
                () =>
                {
                    if (GlobalAppProperties.UserIsManager)
                        RegionManager.RequestNavigateContentRegion<SpecificationView>(new NavigationParameters { { nameof(Specification), SelectedItem } });
                },
                () => this.SelectedItem != null);

            RemoveItemCommand = new DelegateLogConfirmationCommand(
                this.Container.Resolve<IMessageService>(),
                "Вы уверены, что хотите удалить данную спецификацию?",
                () =>
                {
                    var unitOfWork = Container.Resolve<IUnitOfWork>();
                    unitOfWork.RemoveEntity(this.SelectedItem);
                    Container.Resolve<IEventAggregator>().GetEvent<AfterRemoveSpecificationEvent>().Publish(this.SelectedItem);
                },
                () => this.SelectedItem != null);

            this.SelectedLookupChanged += lookup =>
            {
                ((DelegateLogConfirmationCommand) RemoveItemCommand).RaiseCanExecuteChanged();
                ((DelegateLogCommand) EditItemCommand).RaiseCanExecuteChanged();
            };
        }

        public override IEnumerable<SpecificationLookup> GetAllLookups()
        {
            if (GlobalAppProperties.UserIsManager)
                return ((ISalesUnitRepository) UnitOfWork.Repository<SalesUnit>())
                    .GetAllOfCurrentUser()
                    .Where(salesUnit => salesUnit.Specification != null)
                    .Select(salesUnit => salesUnit.Specification)
                    .Distinct()
                    .Select(specification => new SpecificationLookup(specification));

            return UnitOfWork.Repository<Specification>()
                .GetAll()
                .Select(specification => new SpecificationLookup(specification));
        }
    }
}