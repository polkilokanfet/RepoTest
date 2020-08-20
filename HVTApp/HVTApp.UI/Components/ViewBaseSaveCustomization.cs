using System;
using System.IO;
using System.Windows;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model;
using Infragistics.Windows.DataPresenter;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Components
{
    public abstract class ViewBaseSaveCustomization : HVTApp.Infrastructure.ViewBase
    {
        protected abstract XamDataGrid DataGrid { get; }

        private string PathToCustomFile => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), FileName);
        private string PathToCustomFileDeveloper => Path.Combine(GlobalAppProperties.Actual.IncomingRequestsPath, $"{FileName}Developer");

        private string FileName => this.GetType().Name;

        protected ViewBaseSaveCustomization() : base() { }

        protected ViewBaseSaveCustomization(LoadableExportableExpandCollapseViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            this.DataContext = viewModel;

            //сохранение кастомизации вида
            viewModel.SaveGridCustomisationEvent += SaveGridCustomisations;

            //подписка на событие раскрытия всех юнитов
            viewModel.ExpandCollapseEvent += ExpandCollapseMethod;

            this.Loaded += OnLoaded;
            viewModel.Loaded += () =>
            {
                ExpandCollapseMethod(true);
                ExpandCollapseMethod(false);
            };
        }

        private void ExpandCollapseMethod(bool expand)
        {
            if (DataGrid == null)
                return;

            foreach (var o in DataGrid.DataSource)
            {
                DataGrid.GetRecordFromDataItem(o, recursive: false).IsExpanded = expand;
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            LoadGridCustomisations();
            this.Loaded -= OnLoaded;
        }


        private void SaveGridCustomisations()
        {
            using (var fs = new FileStream(PathToCustomFile, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                DataGrid.SaveCustomizations(fs);
            }

            //если разработчик сохраняет
            if (GlobalAppProperties.User.Id == GlobalAppProperties.Actual.Developer.Id)
            {               
                using (var fs = new FileStream(PathToCustomFileDeveloper, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    DataGrid.SaveCustomizations(fs);
                }
            }
        }

        private void LoadGridCustomisations()
        {
            if (DataGrid == null)
                return;

            //загрузка личных настроек
            if (File.Exists(PathToCustomFile))
            {
                using (var fs = new FileStream(PathToCustomFile, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    DataGrid.LoadCustomizations(fs);
                }
                return;
            }

            //загрузка настроек разработчика
            if (File.Exists(PathToCustomFileDeveloper))
            {
                using (var fs = new FileStream(PathToCustomFileDeveloper, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    DataGrid.LoadCustomizations(fs);
                }
            }
        }

    }
}