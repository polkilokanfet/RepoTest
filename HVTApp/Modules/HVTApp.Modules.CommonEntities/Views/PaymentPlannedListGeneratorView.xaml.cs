using HVTApp.Infrastructure;
using HVTApp.UI.Tabs;
using HVTApp.UI.ViewModels;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Views
{
    [RibbonTab(typeof(TabCRUD))]
    public partial class PaymentPlannedListGeneratorView 
    {
        public PaymentPlannedListGeneratorView(PaymentPlannedListGeneratorViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
