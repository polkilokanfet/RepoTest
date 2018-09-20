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
            return false;
        }

        public override async void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            //по шаблону-проекту
            var project = navigationContext.Parameters.First().Value as Project;
            if (project != null) await _viewModel.LoadByProject(project);

            //по шаблону-предложению
            var offer = navigationContext.Parameters.First().Value as Offer;
            if (offer != null)
            {
                if (navigationContext.Parameters.Count() == 1) await _viewModel.LoadByOffer(offer);
                else await _viewModel.LoadAsync(offer);
            }
        }

        public override void OnNavigatedFrom(NavigationContext navigationContext)
        {
            if(_viewModel.SaveCommand.CanExecute(null))
            {
                if (_container.Resolve<IMessageService>().ShowYesNoMessageDialog("Сохранение", "Сохранить изменения?") == MessageDialogResult.Yes)
                {
                    _viewModel.SaveCommand.Execute(null);
                }
            }

            base.OnNavigatedFrom(navigationContext);
        }
    }
}
