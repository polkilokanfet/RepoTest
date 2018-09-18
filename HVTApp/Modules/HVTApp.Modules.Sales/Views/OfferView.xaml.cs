using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Modules.Sales.Tabs;
using HVTApp.Modules.Sales.ViewModels;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.Modules.Sales.Views
{
    [RibbonTab(typeof(TabCrudUnits))]
    public partial class OfferView
    {
        private readonly OfferViewModel _viewModel;

        public OfferView(IUnityContainer container, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            _viewModel = container.Resolve<OfferViewModel>();
            this.DataContext = _viewModel;
        }

        public override async void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            //по шаблону-проекту
            var project = navigationContext.Parameters.First().Value as Project;
            if (project != null) await _viewModel.LoadByProject(project);

            //по шаблону-предложению
            var offer = navigationContext.Parameters.First().Value as Offer;
            if (offer != null) await _viewModel.LoadByOffer(offer);
        }

    }
}
