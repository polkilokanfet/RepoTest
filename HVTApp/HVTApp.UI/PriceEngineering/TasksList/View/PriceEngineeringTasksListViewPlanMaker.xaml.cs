using HVTApp.Infrastructure;
using HVTApp.UI.PriceEngineering.Tabs;
using HVTApp.UI.PriceEngineering.ViewModel;
using Prism.Events;
using Prism.Regions;
using System.ComponentModel;
using System.Windows;

namespace HVTApp.UI.PriceEngineering.View
{
    [RibbonTab(typeof(TabPriceEngineeringTasks))]
    public partial class PriceEngineeringTasksListViewPlanMaker
    {

        public PriceEngineeringTasksListViewPlanMaker(PriceEngineeringTasksListViewModelPlanMaker priceEngineeringTasksListViewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = priceEngineeringTasksListViewModel;
            this.Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is INotifyPropertyChanged propertyChangedViewModel)
            {
                if (propertyChangedViewModel is IIsShownActual viewModel)
                {
                    propertyChangedViewModel.PropertyChanged += (o, eventArgs) =>
                    {
                        if (eventArgs.PropertyName == nameof(viewModel.IsShownActual))
                        {
                            this.DataGrid.SetFilter("ToShow", viewModel.IsShownActual, true);
                        }
                    };

                    this.DataGrid.SetFilter("ToShow", viewModel.IsShownActual, true);
                }
            }

            this.Loaded -= OnLoaded;
        }

    }
}
