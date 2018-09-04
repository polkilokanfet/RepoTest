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
            var offerViewModel = container.Resolve<OfferDetailsViewModel>();
            var offerDetailsView = container.Resolve<OfferDetailsView>();
            offerDetailsView.DataContext = offerViewModel;
            DetailsControl.Content = offerDetailsView;
            this.DataContext = offerViewModel;
        }
    }
}
