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
    public partial class SpecificationView
    {
        private readonly SpecificationViewModel _viewModel;
        public SpecificationView(IUnityContainer container, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            _viewModel = container.Resolve<SpecificationViewModel>();
            this.DataContext = _viewModel;
        }

        public override bool IsNavigationTarget(NavigationContext navigationContext)
        {
            var specification = navigationContext.Parameters.First().Value as Specification;
            return _viewModel.Item != null && specification != null && _viewModel.Item.Id == specification.Id;
        }

        public override async void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            if (IsNavigationTarget(navigationContext)) return;

            var project = navigationContext.Parameters.First().Value as Project;
            var specification = navigationContext.Parameters.First().Value as Specification;

            if (project != null) await _viewModel.LoadAsync(project);
            if (specification != null) await _viewModel.LoadAsync(specification);
        }
    }
}

