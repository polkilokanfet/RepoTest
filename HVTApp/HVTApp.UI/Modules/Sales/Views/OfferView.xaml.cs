using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.UI.Modules.Sales.Tabs;
using HVTApp.UI.Modules.Sales.ViewModels;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Sales.Views
{
    [RibbonTab(typeof(TabCrudUnits))]
    public partial class OfferView
    {
        private readonly OfferViewModel _viewModel;

        public OfferView(IUnityContainer container, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            _viewModel = container.Resolve<OfferViewModel>();
            InitializeComponent();
            this.DataContext = _viewModel;
        }

        public override bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        public override async void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            //if (IsNavigationTarget(navigationContext)) return;

            //по шаблону-проекту
            var project = navigationContext.Parameters.First().Value as Project;
            if (project != null) await _viewModel.LoadAsync(new Offer(), true, project);

            
            var offer = navigationContext.Parameters.First().Value as Offer;
            if (offer != null)
            {
                //по шаблону-предложению
                if (navigationContext.Parameters.Count() == 1) await _viewModel.LoadAsync(new Offer(), true, offer);
                //для изменения
                else await _viewModel.LoadAsync(offer, false);
            }
        }
    }
}
