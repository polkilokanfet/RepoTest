using System.IO;
using HVTApp.Infrastructure;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Sales.Payments
{
    [RibbonTab(typeof(PaymentsTab))]
    public partial class PaymentsView
    {
        public PaymentsView(PaymentsViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            this.DataContext = viewModel;
            viewModel.SaveGridCustomisationEvent += SaveGridCustomisations;
            LoadGridCustomisations();

            viewModel.ExpandCollapseEvent += expend =>
            {
                var dg = this.PaymentsGrid; //(XamDataGrid)sender;
                foreach (var o in dg.DataSource)
                {
                    dg.GetRecordFromDataItem(o, recursive: false).IsExpanded = expend;
                }
            };

        }

        string fileName = "paymentsPlannedSalesCustomisation.xml";
        private void SaveGridCustomisations()
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            using (var fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                PaymentsGrid.SaveCustomizations(fs);
            }
        }

        private void LoadGridCustomisations()
        {
            if (File.Exists(fileName))
            {
                using (var fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    PaymentsGrid.LoadCustomizations(fs);
                }
            }
        }

    }
}
