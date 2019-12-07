using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;
using HVTApp.UI.Modules.PlanAndEconomy.Views;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Regions;

namespace HVTApp.UI.Modules.PlanAndEconomy.ViewModels
{
    public class OrderItem
    {
        private readonly List<SalesUnit> _salesUnits;

        public Product Product { get; }
        public int Amount => _salesUnits.Count;
        public DateTime EndProductionPlanDate { get; }
        public int EndProductionPlanDateYear { get; }
        public int EndProductionPlanDateMonth { get; }
        public Order Order { get; }

        public OrderItem(IEnumerable<SalesUnit> salesUnits)
        {
            _salesUnits = salesUnits.ToList();
            Product = _salesUnits.First().Product;
            EndProductionPlanDate = _salesUnits.First().EndProductionPlanDate.Value;
            EndProductionPlanDateYear = EndProductionPlanDate.Year;
            EndProductionPlanDateMonth = EndProductionPlanDate.Month;
            Order = _salesUnits.First().Order;
        }
    }

    public class ProductionPlanViewModel : ViewModelBase
    {
        private OrderItem _selectedOrderItem;
        public ObservableCollection<OrderItem> OrderItems { get; }

        public OrderItem SelectedOrderItem
        {
            get { return _selectedOrderItem; }
            set
            {
                _selectedOrderItem = value;
                ((DelegateCommand)EditOrderCommand).RaiseCanExecuteChanged();
            }
        }

        public ICommand NewOrderCommand { get; }
        public ICommand EditOrderCommand { get; }

        public ProductionPlanViewModel(IUnityContainer container) : base(container)
        {
            var salesUnits = UnitOfWork.Repository<SalesUnit>().Find(x => x.EndProductionPlanDate != null);
            var groups = salesUnits.GroupBy(x => new
            {
                Product = x.Product.Id,
                EndProductionPlanDate = x.EndProductionPlanDate,
                Order = x.Order.Id
            }).OrderBy(x => x.Key.EndProductionPlanDate);

            OrderItems = new ObservableCollection<OrderItem>(groups.Select(x => new OrderItem(x)));

            NewOrderCommand = new DelegateCommand(
                () =>
                {
                    RequestNavigate(new Order());
                });
            EditOrderCommand = new DelegateCommand(
                () => { RequestNavigate(SelectedOrderItem.Order); }, 
                () => SelectedOrderItem != null);
        }

        private void RequestNavigate(Order order)
        {
            Container.Resolve<IRegionManager>().RequestNavigateContentRegion<OrderView>(new NavigationParameters { { "", order } });
        }

    }
}
