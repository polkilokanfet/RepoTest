using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.UI.ViewModels;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Views
{
    public partial class OfferView
    {
        private OfferDetailsViewModel _viewModel;

        public OfferView(IUnityContainer container, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            _viewModel = container.Resolve<OfferDetailsViewModel>();
            var offerDetailsView = container.Resolve<OfferDetailsView>();
            offerDetailsView.DataContext = _viewModel;
            DetailsControl.Content = offerDetailsView;
            this.DataContext = _viewModel;
        }

        public override async void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            var offer = (Offer)navigationContext.Parameters.First().Value;
            await _viewModel.LoadAsync(offer);
        }

    }
}
