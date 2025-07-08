using HVTApp.Infrastructure;
using HVTApp.UI.Modules.BookRegistration.Tabs;
using HVTApp.UI.Modules.BookRegistration.ViewModels;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.BookRegistration.Views
{
    [RibbonTab(typeof(TabBookRegistration))]
    public partial class BookRegistrationView
    {
        public BookRegistrationView(BookRegistrationViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
