using System;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.UI.PriceEngineering.Tabs;
using HVTApp.UI.PriceEngineering.ViewModel;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.PriceEngineering.View
{
    [RibbonTab(typeof(TabPriceEngineeringTask))]
    public partial class PriceEngineeringTasksViewBackManager : IDisposable
    {
        private TasksViewModelConstructor _viewModel;

        public PriceEngineeringTasksViewBackManager(TasksViewModelConstructor viewModel, IUnityContainer container, IRegionManager regionManager, IEventAggregator eventAggregator) : base(container, regionManager, eventAggregator)
        {
            _viewModel = viewModel;
            InitializeComponent();
            this.DataContext = _viewModel;
        }

        public override bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters.Any())
            {
                if (navigationContext.Parameters.Count() == 1)
                {
                    if (navigationContext.Parameters.First().Value is PriceEngineeringTasks priceEngineeringTasks)
                        _viewModel.Load(priceEngineeringTasks);

                    if (navigationContext.Parameters.First().Value is PriceEngineeringTask priceEngineeringTask)
                        _viewModel.Load(priceEngineeringTask);
                }
            }

            base.OnNavigatedTo(navigationContext);
        }

        public override void OnNavigatedFrom(NavigationContext navigationContext)
        {
            base.OnNavigatedFrom(navigationContext);
            this.Dispose();
        }

        protected override bool IsSomethingChanged()
        {
            return false;
            //return _viewModel.PriceEngineeringTasksWrapper != null && _viewModel.PriceEngineeringTasksWrapper.IsChanged;
        }

        public void Dispose()
        {
            _viewModel.Dispose();
            _viewModel = null;
            this.DataContext = null;

            GC.SuppressFinalize(this);
        }
    }
}
