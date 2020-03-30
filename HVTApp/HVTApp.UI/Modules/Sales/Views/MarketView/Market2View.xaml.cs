using System;
using System.IO;
using System.Windows;
using HVTApp.Infrastructure;
using HVTApp.UI.Modules.Sales.Tabs;
using HVTApp.UI.Modules.Sales.ViewModels;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Sales.Views.MarketView
{
    [RibbonTab(typeof(MarketTab))]
    [RibbonTab(typeof(MarketViewTab))]
    [RibbonTab(typeof(MarketSettingsTab))]
    public partial class Market2View : ViewBase
    {
        private Uri _currentUri;

        public Market2View(Market2ViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();

            //назначаем контексты
            this.DataContext = viewModel;

            viewModel.ExpandCollapseEvent += expend =>
            {
                var dg = this.ProjectsGrid; //(XamDataGrid)sender;
                foreach (var o in dg.DataSource)
                {
                    dg.GetRecordFromDataItem(o, recursive: false).IsExpanded = expend;
                }
            };
            
            viewModel.SaveGridCustomisationsEvent += SaveGridCustomisations;
            LoadGridCustomisations();

            //приложения проекта
            viewModel.SelectedProjectItemChanged += projectItem =>
            {
                if (projectItem != null)
                {
                    _currentUri = new Uri(PathGetter.GetPath(projectItem.Project));
                    this.Browser.Source = _currentUri;
                }
            };
        }

        string fileName = "projectsGridCustomisation.xml";
        private void SaveGridCustomisations()
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                ProjectsGrid.SaveCustomizations(fs);
            }
        }

        private void LoadGridCustomisations()
        {
            if (File.Exists(fileName))
            {
                using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    ProjectsGrid.LoadCustomizations(fs);
                }
            }
        }

        private void GoBackButton_OnClick(object sender, RoutedEventArgs e)
        {
            if(this.Browser.CanGoBack && this.Browser.Source != _currentUri)
                Browser.GoBack();
        }

        private void GoForwardButton_OnClick(object sender, RoutedEventArgs e)
        {
            if(this.Browser.CanGoForward)
                Browser.GoForward();
        }
    }
}
