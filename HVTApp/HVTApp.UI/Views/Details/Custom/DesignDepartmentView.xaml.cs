using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.UI.ViewModels;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Views
{
    public partial class DesignDepartmentView
    {
        private readonly DesignDepartmentViewModel _viewModel;

        public DesignDepartmentView(DesignDepartmentViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            _viewModel = viewModel;
            InitializeComponent();
            this.DataContext = viewModel;
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            if (navigationContext.Parameters.Any())
            {
                if (navigationContext.Parameters.First().Value is DesignDepartment designDepartment)
                {
                    _viewModel.Load(designDepartment.Id);
                }
            }
            else
            {
                _viewModel.Load(new DesignDepartment());
            }
        }
    }
}
