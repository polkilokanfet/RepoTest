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
            viewModel.SaveGridCustomisationEvent += () =>
            {
                using (var fs = new FileStream(PathToCustomFile, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    DataGrid.SaveCustomizations(fs);
                }

                //если разработчик сохран€ет
                if (GlobalAppProperties.User.Id == GlobalAppProperties.Actual.Developer.Id)
                {
                    using (var fs = new FileStream(PathToCustomFileDeveloper, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        DataGrid.SaveCustomizations(fs);
                    }
                }
            };

            //подписка на событие раскрыти€ всех юнитов
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


        private void LoadGridCustomisations()
        {
            if (DataGrid == null)
                return;

            //загрузка личных настроек
            if (!LoadCustomisation(PathToCustomFile))
            {
                //загрузка настроек разработчика
                LoadCustomisation(PathToCustomFileDeveloper);
            }
        }

        /// <summary>
        /// «агрузка настроек таблицы
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private bool LoadCustomisation(string path)
        {
            if (!File.Exists(path)) return false;

            using (var fs = new FileStream(PathToCustomFileDeveloper, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                DataGrid.LoadCustomizations(fs);
            }

            return true;
        }
    }
}