﻿using System.IO;
using HVTApp.Infrastructure;
using HVTApp.UI.Modules.Director.Tabs;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Reports.SalesReport
{
    [RibbonTab(typeof(TabReload))]
    public partial class SalesReportView
    {
        public SalesReportView(SalesReportViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            this.DataContext = viewModel;
            viewModel.SaveGridCustomisationEvent += SaveGridCustomisations;
            LoadGridCustomisations();
        }

        string fileName = "salesReportCustomisation.xml";
        private void SaveGridCustomisations()
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                ReportGrid.SaveCustomizations(fs);
            }
        }

        private void LoadGridCustomisations()
        {
            if (File.Exists(fileName))
            {
                using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    ReportGrid.LoadCustomizations(fs);
                }
            }
        }
    }
}