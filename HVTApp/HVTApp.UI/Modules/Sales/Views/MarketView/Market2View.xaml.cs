using System.IO;
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
        //private readonly Market2ViewModel _viewModel;

        public Market2View(Market2ViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();

            //назначаем контексты
            //_viewModel = viewModel;
            //viewModel.Load();
            this.DataContext = viewModel;

            //this.Loaded += OnLoaded;

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


        //private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        //{
        //    _viewModel.Load();
        //    this.Loaded -= OnLoaded;
        //}
    }
}
