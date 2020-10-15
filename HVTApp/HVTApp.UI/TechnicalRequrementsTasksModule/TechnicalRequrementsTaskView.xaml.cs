using System.Collections.Generic;
using System.Linq;
using System.Windows;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.UI.TechnicalRequrementsTasksModule.Wrapper;
using Infragistics.Windows.DataPresenter;
using Infragistics.Windows.Editors;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.TechnicalRequrementsTasksModule
{
    [RibbonTab(typeof(Tabs.TabTechnicalRequrementsTask))]
    public partial class TechnicalRequrementsTaskView : HVTApp.Infrastructure.ViewBase
    {
        private readonly TechnicalRequrementsTaskViewModel _viewModel;
        public TechnicalRequrementsTaskView(TechnicalRequrementsTaskViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            _viewModel = viewModel;
            this.DataContext = viewModel;
        }

        public override bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            if (navigationContext.Parameters.First().Value is TechnicalRequrementsTask)
            {
                var technicalRequrementsTask = navigationContext.Parameters.First().Value as TechnicalRequrementsTask;
                _viewModel.Load(technicalRequrementsTask);
            }

            if (navigationContext.Parameters.First().Value is IEnumerable<SalesUnit>)
            {
                var salesUnits = navigationContext.Parameters.First().Value as IEnumerable<SalesUnit>;
                _viewModel.Load(salesUnits);
            }

            //var dg = this.Groups; //(XamDataGrid)sender;
            //foreach (var o in dg.DataSource)
            //{
            //    dg.GetRecordFromDataItem(o, recursive: false).IsExpanded = true;
            //}
        }

        void IsCheckedValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (Equals(e.OldValue, e.NewValue))
                return;

            var editor = sender as XamCheckEditor;
            ((TechnicalRequrements2Wrapper)(((DataRecord)editor.DataContext).DataItem)).IsChecked = editor.IsChecked.Value;

            ((DelegateCommand) _viewModel.MeregeCommand).RaiseCanExecuteChanged();
        }

    }
}
