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
    public partial class OfferView : ViewBaseConfirmNavigationRequest
    {
        private readonly OfferViewModel _viewModel;

        public OfferView(IUnityContainer container, IRegionManager regionManager, IEventAggregator eventAggregator) : base(container, regionManager, eventAggregator)
        {
            _viewModel = container.Resolve<OfferViewModel>();
            InitializeComponent();
            this.DataContext = _viewModel;
        }

        public override bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            //if (IsNavigationTarget(navigationContext)) return;

            //по шаблону-проекту
            if (navigationContext.Parameters.First().Value is Project project)
            {
                _viewModel.Load(new Offer(), true, project);
            }


            if (navigationContext.Parameters.First().Value is Offer offer)
            {
                //по шаблону-предложению
                if (navigationContext.Parameters.Count() == 1)
                    _viewModel.Load(new Offer(), true, offer);
                //для изменения
                else _viewModel.Load(offer, false);
            }
        }

        protected override bool IsSomethingChanged()
        {
            return _viewModel.DetailsViewModel.Item.IsChanged || _viewModel.GroupsViewModel.IsChanged;
        }
    }
}
