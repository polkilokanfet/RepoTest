﻿using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;
using HVTApp.UI.Modules.PlanAndEconomy.Views;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Regions;

namespace HVTApp.UI.Modules.PlanAndEconomy.ViewModels
{
    public class ProductionPlanViewModel : ViewModelBase
    {
        private OrderItem _selectedOrderItem;
        public ObservableCollection<OrderItem> OrderItems { get; } = new ObservableCollection<OrderItem>();

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
        public ICommand ReloadCommand { get; }

        public ProductionPlanViewModel(IUnityContainer container) : base(container)
        {
            Load();

            NewOrderCommand = new DelegateCommand(
                () =>
                {
                    RequestNavigate(new Order());
                });

            EditOrderCommand = new DelegateCommand(
                () => { RequestNavigate(SelectedOrderItem.Order); }, 
                () => SelectedOrderItem != null);

            ReloadCommand = new DelegateCommand(Load);
        }

        private void Load()
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();
            var salesUnits = UnitOfWork.Repository<SalesUnit>().Find(x => x.Order != null);
            var groups = salesUnits.GroupBy(x => new
            {
                ProductId = x.Product.Id,
                EndProductionPlanDate = x.EndProductionPlanDate,
                OrderId = x.Order.Id
            }).OrderBy(x => x.Key.EndProductionPlanDate);

            OrderItems.Clear();
            OrderItems.AddRange(groups.Select(x => new OrderItem(x)));
        }

        private void RequestNavigate(Order order)
        {
            Container.Resolve<IRegionManager>().RequestNavigateContentRegion<OrderView>(new NavigationParameters { { nameof(Order), order } });
        }

    }
}