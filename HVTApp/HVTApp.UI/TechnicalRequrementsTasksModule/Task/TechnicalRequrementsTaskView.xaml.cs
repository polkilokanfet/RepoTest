using System.Collections.Generic;
using System.Linq;
using System.Windows;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
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
        public TechnicalRequrementsTaskViewModel ViewModel1 { get; }
        public TechnicalRequrementsTaskView(TechnicalRequrementsTaskViewModel viewModel1, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            ViewModel1 = viewModel1;
            InitializeComponent();
            this.DataContext = viewModel1;
        }

        public override bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            if (navigationContext.Parameters.First().Value is TechnicalRequrementsTask technicalRequrementsTask)
            {
                ViewModel1.Load(technicalRequrementsTask);
            }

            if (navigationContext.Parameters.First().Value is IEnumerable<SalesUnit> salesUnits)
            {
                ViewModel1.Load(salesUnits);
            }

            //var dg = this.Groups; //(XamDataGrid)sender;
            //foreach (var o in dg.DataSource)
            //{
            //    dg.GetRecordFromDataItem(o, recursive: false).IsExpanded = true;
            //}
        }

        void IsCheckedValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (Equals(e.OldValue, e.NewValue)) return;

            var editor = sender as XamCheckEditor;
            ((TechnicalRequrements2Wrapper)(((DataRecord)editor.DataContext).DataItem)).IsChecked = editor.IsChecked.Value;

            ViewModel1.MergeCommand.RaiseCanExecuteChanged();
        }

        void IsActualValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (Equals(e.OldValue, e.NewValue)) return;

            var checkEditor = (XamCheckEditor)sender;
            if (((DataRecord)checkEditor.DataContext).DataItem is TechnicalRequrementsFileWrapper)
            {
                if(((TechnicalRequrementsFileWrapper)((DataRecord)checkEditor.DataContext).DataItem).IsActual != checkEditor.IsChecked.Value)
                    ((TechnicalRequrementsFileWrapper)((DataRecord)checkEditor.DataContext).DataItem).IsActual = checkEditor.IsChecked.Value;
            }

            else if (((DataRecord)checkEditor.DataContext).DataItem is TechnicalRequrements2Wrapper)
            {
                if(((TechnicalRequrements2Wrapper)((DataRecord)checkEditor.DataContext).DataItem).IsActual != checkEditor.IsChecked.Value)
                    ((TechnicalRequrements2Wrapper)((DataRecord)checkEditor.DataContext).DataItem).IsActual = checkEditor.IsChecked.Value;
            }

            ViewModel1.StartCommand.RaiseCanExecuteChanged();
        }

    }
}
