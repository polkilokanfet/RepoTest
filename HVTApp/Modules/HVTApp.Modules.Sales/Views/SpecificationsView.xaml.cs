using HVTApp.Infrastructure;
using HVTApp.Modules.Sales.ViewModels;
using HVTApp.UI.Tabs;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.Modules.Sales.Views
{
    [RibbonTab(typeof(TabCRUD))]
    public partial class SpecificationsView
    {
        public SpecificationsView(SpecificationsViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            viewModel.LoadSpecifications();
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
