using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Modules.PlanAndEconomy.Views;
using HVTApp.UI.Lookup;
using HVTApp.UI.ViewModels;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Regions;

namespace HVTApp.Modules.PlanAndEconomy.ViewModels
{
    public class OrdersViewModel : OrderLookupListViewModel
    {
        public OrdersViewModel(IUnityContainer container) : base(container)
        {
            NewItemCommand = new DelegateCommand(() => RequestNavigate(new Order()));
            EditItemCommand = new DelegateCommand(() => RequestNavigate(SelectedItem), () => SelectedItem != null);
            RemoveItemCommand = new DelegateCommand(RemoveOrderCommand_ExecuteAsync, () => SelectedItem != null);
        }

        private void RequestNavigate(Order order)
        {
            Container.Resolve<IRegionManager>().RequestNavigateContentRegion<OrderView>(new NavigationParameters { { "", order } });
        }

        private async void RemoveOrderCommand_ExecuteAsync()
        {
            var dr = MessageService.ShowYesNoMessageDialog("Удаление", $"Вы действительно хотите удалить \"{SelectedLookup.DisplayMember}\"?");
            if (dr != MessageDialogResult.Yes) return;

            var unitOfWork = Container.Resolve<IUnitOfWork>();

            var order = await unitOfWork.Repository<Order>().GetByIdAsync(SelectedItem.Id);
            var salesUnits = (await unitOfWork.Repository<SalesUnit>().GetAllAsync()).Where(x => Equals(x.Order, order));
            salesUnits.ForEach(x => x.Order = null);

            try
            {
                unitOfWork.Repository<Order>().Delete(order);
                await unitOfWork.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                MessageService.ShowOkMessageDialog(e.GetType().ToString(), e.GetAllExceptions());
                return;
            }

            EventAggregator.GetEvent<AfterRemoveOrderEvent>().Publish(order);
        }

    }
}
