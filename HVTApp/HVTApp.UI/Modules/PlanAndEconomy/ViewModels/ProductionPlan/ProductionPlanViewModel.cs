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
            var salesUnits = UnitOfWork.Repository<SalesUnit>().Find(x => x.Order != null);
            var groups = salesUnits.GroupBy(x => new
            {
                ProductId = x.Product.Id,
                EndProductionPlanDate = x.EndProductionPlanDate,
                OrderId = x.Order.Id
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
            Container.Resolve<IRegionManager>().RequestNavigateContentRegion<OrderView>(new NavigationParameters { { nameof(Order), order } });
        }

    }
}
