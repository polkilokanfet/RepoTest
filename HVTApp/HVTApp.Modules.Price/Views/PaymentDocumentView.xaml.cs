using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Modules.PlanAndEconomy.ViewModels;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.Modules.PlanAndEconomy.Views
{
    public partial class PaymentDocumentView : ViewBase
    {
        private readonly PaymentDocumentViewModel _viewModel;
        public PaymentDocumentView(PaymentDocumentViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            _viewModel = viewModel;
            this.DataContext = viewModel;
        }

        public override bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        public override async void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            var paymentDocument = navigationContext.Parameters.First().Value as PaymentDocument;
            await _viewModel.LoadAsync(paymentDocument);
        }
    }
}
