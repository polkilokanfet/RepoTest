using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Specifications
{
    [RibbonTab(typeof(TabSpecifications))]
    public partial class SpecificationsViewBase
    {
        private readonly SpecificationsViewModelBase _viewModel;

        public SpecificationsViewBase(SpecificationsViewModelBase viewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            _viewModel = viewModel;
            this.DataContext = _viewModel;
            InitializeComponent();
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            if (navigationContext.Parameters.Any() == false)
            {
                _viewModel.Load();
            }
        }
    }
}