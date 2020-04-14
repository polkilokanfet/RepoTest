using System;
using System.Linq;
using HVTApp.Infrastructure;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Directum
{
    [RibbonTab(typeof(TabDirectumTask))]
    public partial class DirectumTaskView : ViewBaseConfirmNavigationRequest
    {
        private readonly DirectumTaskViewModel _viewModel;

        public DirectumTaskView(DirectumTaskViewModel viewModel, IUnityContainer container, IRegionManager regionManager, IEventAggregator eventAggregator) : base(container, regionManager, eventAggregator)
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
                    var directumTask = navigationContext.Parameters.First().Value as Model.POCOs.DirectumTask;
                    if (directumTask != null)
                        _viewModel.Load(directumTask);

                    var directumTaskGroup = navigationContext.Parameters.First().Value as Model.POCOs.DirectumTaskGroup;
                    if (directumTaskGroup != null)
                        _viewModel.Load(directumTaskGroup);
                }

                if (navigationContext.Parameters.Count() == 2)
                {
                    var parentTask = (Model.POCOs.DirectumTask)navigationContext.Parameters[nameof(Model.POCOs.DirectumTask)];
                    var isSubTask = (bool) navigationContext.Parameters[nameof(bool.GetType)];
                    _viewModel.Load(parentTask, isSubTask);
                }
            }
            else
            {
                _viewModel.Load();
            }

            base.OnNavigatedTo(navigationContext);
        }


        protected override bool IsSomethingChanged()
        {
            return _viewModel.DirectumTask?.IsChanged ?? false;
        }
    }
}
