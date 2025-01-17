using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.UI.Modules.Sales.Tabs;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Sales.Project1
{
    [RibbonTab(typeof(TabCrudUnitsInProject))]
    public partial class ProjectView : ViewBaseConfirmNavigationRequest
    {
        private readonly ProjectViewModel _viewModel;

        public ProjectView(IUnityContainer container, IRegionManager regionManager, IEventAggregator eventAggregator) : base(container, regionManager, eventAggregator)
        {
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

            //если грузится существующий проект
            if (navigationContext.Parameters != null && navigationContext.Parameters.Any())
            {
                if (navigationContext.Parameters.Count() == 1)
                {
                    //загрузка проекта
                    if (navigationContext.Parameters.First().Value is Model.POCOs.Project)
                    {
                        var project = (Model.POCOs.Project)navigationContext.Parameters.First().Value;
                        _viewModel.Load(project, false);
                    }

                    //перенос оборудования в новый проект
                    if (navigationContext.Parameters.First().Value is List<SalesUnit>)
                    {
                        var units = (List<SalesUnit>)navigationContext.Parameters.First().Value;
                        _viewModel.LoadForMove(units);
                    }
                }
                else if (navigationContext.Parameters.Count() == 2)
                {
                    //перенос оборудования в существующий проект
                    var project = (Model.POCOs.Project)navigationContext.Parameters.First().Value;
                    var units = (List<SalesUnit>)navigationContext.Parameters.Last().Value;
                    _viewModel.LoadForMove(units, project);
                }
            }
            //если грузится новый проект
            else
            {
                _viewModel.Load(new Model.POCOs.Project(), true);
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

