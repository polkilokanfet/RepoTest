using HVTApp.UI.ViewModels;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Views
{
    public partial class OrderView
    {
        public OrderView(IUnityContainer container, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            var view = container.Resolve<OrderDetailsView>();
            var viewModel = container.Resolve<OrderDetailsViewModel>();
            view.DataContext = viewModel;
            DetailsControl.Content = view;
            this.DataContext = viewModel;
        }
    }
}
