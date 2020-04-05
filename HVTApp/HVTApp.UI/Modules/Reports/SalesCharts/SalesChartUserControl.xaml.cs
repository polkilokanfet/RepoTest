using System.Windows.Controls;
using Infragistics.Controls.Charts;

namespace HVTApp.UI.Modules.Reports.SalesCharts
{
    /// <summary>
    /// Interaction logic for SalesChartUserControl.xaml
    /// </summary>
    public partial class SalesChartUserControl : UserControl
    {
        public SalesChartUserControl()
        {
            InitializeComponent();
        }

        private void pieChart_SliceClick(object sender, SliceClickEventArgs e)
        {
            e.IsExploded = !e.IsExploded;
        }

    }
}
