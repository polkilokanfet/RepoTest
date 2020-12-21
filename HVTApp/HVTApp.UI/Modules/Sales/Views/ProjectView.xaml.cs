using System;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.POCOs;
using HVTApp.UI.Modules.Sales.Tabs;
using HVTApp.UI.Modules.Sales.ViewModels.ProjectViewModel;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Sales.Views
{
    [RibbonTab(typeof(TabCrudUnits))]
    public partial class ProjectView : ViewBaseConfirmNavigationRequest
    {
        private readonly IUnityContainer _container;
        private readonly ProjectViewModel _viewModel;

        public ProjectView(IUnityContainer container, IRegionManager regionManager, IEventAggregator eventAggregator) : base(container, regionManager, eventAggregator)
        {
            _container = container;
            InitializeComponent();
            _viewModel = container.Resolve<ProjectViewModel>();
            this.DataContext = _viewModel;
        }

        public override bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            if (navigationContext.Parameters != null && navigationContext.Parameters.Any())
            {
                var project = (Project)navigationContext.Parameters.First().Value;
                _viewModel.Load(project, false);
            }
            else
            {
                _viewModel.Load(new Project(), true);
            }
        }

        protected override bool IsSomethingChanged()
        {
            return _viewModel.DetailsViewModel.Item.IsChanged || _viewModel.GroupsViewModel.IsChanged;
        }

        public override void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            ////если придет запрос при несохраненных изменениях
            //if (_viewModel.SaveCommand.CanExecute(null))
            //{
            //    var dr = Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Сохранение", "Сохранить сделанные изменения?", defaultNo: true);
            //    if (dr == MessageDialogResult.Yes)
            //        _viewModel.SaveCommand.Execute(null);
            //}

            //continuationCallback(true);

            continuationCallback(true);
        }
    }
}

