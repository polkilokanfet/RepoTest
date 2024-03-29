using System;
using System.IO;
using System.Windows;
using System.Xml;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Infrastructure.Services;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model;
using Infragistics.Windows.DataPresenter;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Components
{
    public abstract class ViewBaseSaveCustomization : HVTApp.Infrastructure.ViewBase
    {
        private readonly IMessageService _messageService;
        protected abstract XamDataGrid DataGrid { get; }

        private string PathToCustomDirectory => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "HVTAppViewsCustom");
        private string PathToCustomFile => Path.Combine(PathToCustomDirectory, FileName);

        private string PathToCustomDirectoryDeveloper => Path.Combine(GlobalAppProperties.Actual.IncomingRequestsPath, "HVTAppViewsCustom");
        private string PathToCustomFileDeveloper => Path.Combine(PathToCustomDirectoryDeveloper, $"{FileName}Developer");

        private string FileName => this.GetType().Name;

        protected ViewBaseSaveCustomization() : base() { }

        protected ViewBaseSaveCustomization(LoadableExportableExpandCollapseViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator, IMessageService messageService) : base(regionManager, eventAggregator)
        {
            _messageService = messageService;
            this.DataContext = viewModel;

            //���������� ������������ ����
            viewModel.SaveGridCustomisationEvent += () =>
            {
                SaveGridCustomisation(PathToCustomDirectory, PathToCustomFile);
                if (GlobalAppProperties.User.Id == GlobalAppProperties.Actual.Developer.Id)
                    SaveGridCustomisation(PathToCustomDirectoryDeveloper, PathToCustomFileDeveloper);
            };

            //�������� �� ������� ��������� ���� ������
            viewModel.ExpandCollapseEvent += ExpandCollapseMethod;

            this.Loaded += OnLoaded;
            viewModel.LoadComplited += () =>
            {
                ExpandCollapseMethod(true);
                ExpandCollapseMethod(false);
            };
        }

        private void SaveGridCustomisation(string pathDirectory, string pathFile)
        {
            if (!Directory.Exists(pathDirectory))
                Directory.CreateDirectory(pathDirectory);

            using (var fs = new FileStream(pathFile, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                DataGrid.SaveCustomizations(fs);
            }
        }

        protected void ExpandCollapseMethod(bool isExpanded)
        {
            if (DataGrid == null)
                return;

            foreach (var o in DataGrid.DataSource)
            {
                try
                {
                    DataGrid.GetRecordFromDataItem(o, recursive: false).IsExpanded = isExpanded;
                }
                catch (InvalidOperationException)
                {
                    return;
                }
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

            //�������� ������ ��������
            if (!LoadCustomisation(PathToCustomFile))
            {
                //�������� �������� ������������
                LoadCustomisation(PathToCustomFileDeveloper);
            }
        }

        /// <summary>
        /// �������� �������� �������
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private bool LoadCustomisation(string path)
        {
            if (!File.Exists(path))
                return false;

            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                try
                {
                    DataGrid.LoadCustomizations(fs);
                }
                catch (XmlException e)
                {
                    string message = $"���� ��������� ���� {this.GetType().FullName} ���������. ���� � �����: {path} \n" + e.PrintAllExceptions();
                    _messageService.Message(e.GetType().ToString(), message);
                    return false;
                }
            }

            return true;
        }
    }
}