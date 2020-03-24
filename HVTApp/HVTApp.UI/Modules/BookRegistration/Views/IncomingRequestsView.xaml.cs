using System.Windows;
using HVTApp.Infrastructure;
using HVTApp.UI.Modules.BookRegistration.Tabs;
using HVTApp.UI.Modules.BookRegistration.ViewModels;
using HVTApp.UI.Tabs;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.BookRegistration.Views
{
    //[RibbonTab(typeof(TabBookRegistration))]
    public partial class IncomingRequestsView
    {
        public IncomingRequestsView(IncomingRequestsViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
