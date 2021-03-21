using System.Windows;
using HVTApp.Infrastructure;
using HVTApp.UI.Modules.Reports.FlatReport.Containers;
using Infragistics.Windows.DataPresenter;
using Infragistics.Windows.Editors;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Reports.FlatReport
{
    [RibbonTab(typeof(TabFlatReport))]
    public partial class FlatReportView
    {
        public FlatReportView(FlatReportViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }

        //void IsCheckedValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        //{
        //    if (Equals(e.OldValue, e.NewValue))
        //        return;
            
        //    var editor = sender as XamCheckEditor;
        //    var item = (FlatReportItem)((DataRecord)editor.DataContext).DataItem;
        //    if (item.InReport != editor.IsChecked.Value)
        //        item.InReport = editor.IsChecked.Value;
        //}

    }
}
