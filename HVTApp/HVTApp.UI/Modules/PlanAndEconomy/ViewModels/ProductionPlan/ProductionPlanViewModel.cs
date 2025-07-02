using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.DataAccess;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using HVTApp.UI.Modules.PlanAndEconomy.Views;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.PlanAndEconomy.ViewModels
{
    public class ProductionPlanViewModel : LoadableExportableViewModel
    {
        private OrderItem _selectedOrderItem;
        public ObservableCollection<OrderItem> OrderItems { get; } = new ObservableCollection<OrderItem>();

        public OrderItem SelectedOrderItem
        {
            get => _selectedOrderItem;
            set
            {
                _selectedOrderItem = value;
                EditOrderCommand.RaiseCanExecuteChanged();
            }
        }

        public DelegateLogCommand NewOrderCommand { get; }
        public DelegateLogCommand EditOrderCommand { get; }

        public ProductionPlanViewModel(IUnityContainer container) : base(container)
        {
            NewOrderCommand = new DelegateLogCommand(
                () =>
                {
                    RequestNavigate(new Order());
                });

            EditOrderCommand = new DelegateLogCommand(
                () => { RequestNavigate(SelectedOrderItem.Order); }, 
                () => SelectedOrderItem != null);


            Container.Resolve<IEventAggregator>().GetEvent<AfterSaveOrderItemsEvent>().Subscribe(salesUnits =>
            {
                var units = salesUnits.ToList();
                if (units.First().Order == null) return;
                if (OrderItems.Select(orderItem => orderItem.Order).ContainsById(units.First().Order)) return;

                OrderItems.Add(new OrderItem(units));
            });

            container.Resolve<IEventAggregator>().GetEvent<AfterRemoveOrderEvent>().Subscribe(order =>
            {
                var items = OrderItems.Where(orderItem => orderItem.Order.Id == order.Id).ToList();
                items.ForEach(orderItem => this.OrderItems.Remove(orderItem));
            });
        }

        private List<OrderItem> _orderItems;
        protected override void GetData()
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();
            var salesUnits = ((ISalesUnitRepository)UnitOfWork.Repository<SalesUnit>()).GetAllForProductionPlanView().ToList();
            _orderItems = salesUnits
                .GroupBy(salesUnit => new
                {
                    ProductId = salesUnit.Product.Id,
                    salesUnit.EndProductionPlanDate,
                    OrderId = salesUnit.Order.Id
                })
                .Select(units => new OrderItem(units))
                .OrderByDescending(orderItem => orderItem.EndProductionPlanDate)
                .ToList();
        }

        protected override void AfterGetData()
        {
            OrderItems.Clear();
            OrderItems.AddRange(_orderItems);
        }

        private void RequestNavigate(Order order)
        {
            Container.Resolve<IRegionManager>().RequestNavigateContentRegion<OrderView>(new NavigationParameters { { nameof(Order), order } });
        }
    }
}
