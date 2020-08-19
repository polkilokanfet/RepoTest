using System;
using System.IO;
using System.Windows;
using HVTApp.Infrastructure.ViewModels;
using Infragistics.Windows.DataPresenter;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.Infrastructure
{
    public abstract class ViewBaseSaveCustomization : ViewBase
    {
        protected abstract string FileName { get; }
        protected abstract XamDataGrid DataGrid { get; }

        protected ViewBaseSaveCustomization() : base()
        {
        }

        protected ViewBaseSaveCustomization(LoadableExportableExpandCollapseViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            this.DataContext = viewModel;

            viewModel.SaveGridCustomisationEvent += SaveGridCustomisations;

            viewModel.ExpandCollapseEvent += expend =>
            {
                //var dg = (XamDataGrid)this.LoadbleControl.Content; //(XamDataGrid)sender;
                foreach (var o in DataGrid.DataSource)
                {
                    DataGrid.GetRecordFromDataItem(o, recursive: false).IsExpanded = expend;
                }
            };

            this.Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            LoadGridCustomisations();
            this.Loaded -= OnLoaded;
        }


        private void SaveGridCustomisations()
        {
            if (File.Exists(FileName))
            {
                File.Delete(FileName);
            }

            using (var fs = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                DataGrid.SaveCustomizations(fs);
            }
        }

        private void LoadGridCustomisations()
        {
            if (File.Exists(FileName))
            {
                using (var fs = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    DataGrid.LoadCustomizations(fs);
                }
            }
        }

    }
}