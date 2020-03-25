using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.UI.Modules.BookRegistration.Tabs;
using HVTApp.UI.Modules.BookRegistration.ViewModels;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.BookRegistration.Views
{
    [RibbonTab(typeof(TabIncomingRequest))]
    public partial class IncomingRequestView
    {
        private readonly IncomingRequestViewModel _viewModel;

        public IncomingRequestView(IncomingRequestViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            _viewModel = viewModel;
            InitializeComponent();
            DataContext = viewModel;
        }

        public override bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            var request = navigationContext.Parameters.Single(x => x.Value is IncomingRequest).Value as IncomingRequest;
            var unitOfWork = navigationContext.Parameters.Single(x => x.Value is IUnitOfWork).Value as IUnitOfWork;

            _viewModel.Load(request, unitOfWork);

            base.OnNavigatedTo(navigationContext);
        }
    }
}
