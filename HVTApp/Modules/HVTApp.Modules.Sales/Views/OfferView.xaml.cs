using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Services;
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
        private readonly IUnityContainer _container;
        private readonly OfferViewModel _viewModel;

        public OfferView(IUnityContainer container, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            _container = container;
            _viewModel = container.Resolve<OfferViewModel>();
            InitializeComponent();
            this.DataContext = _viewModel;
        }

        public override bool IsNavigationTarget(NavigationContext navigationContext)
        {
            var offer = navigationContext.Parameters.First().Value as Offer;
            return _viewModel.Item != null && offer != null && _viewModel.Item.Id == offer.Id;
        }

        public override async void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            if (IsNavigationTarget(navigationContext)) return;

            //по шаблону-проекту
            var project = navigationContext.Parameters.First().Value as Project;
            if (project != null) await _viewModel.LoadByProject(project);

            
            var offer = navigationContext.Parameters.First().Value as Offer;
            if (offer != null)
            {
                //по шаблону-предложению
                if (navigationContext.Parameters.Count() == 1) await _viewModel.LoadByOffer(offer);
                //для изменения
                else await _viewModel.LoadAsync(offer);
            }
        }
    }
}
