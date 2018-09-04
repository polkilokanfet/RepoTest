using HVTApp.UI.ViewModels;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Views
{
    public partial class OfferView
    {
        public OfferView(IUnityContainer container, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            var offerDetailsViewModel = container.Resolve<OfferDetailsViewModel>();
            var offerDetailsView = container.Resolve<OfferDetailsView>();
            offerDetailsView.DataContext = offerDetailsViewModel;
            DetailsControl.Content = offerDetailsView;
            this.DataContext = offerDetailsViewModel;
        }
    }
}
