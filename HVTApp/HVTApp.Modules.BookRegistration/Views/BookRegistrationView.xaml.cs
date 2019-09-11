using System.Windows;
using HVTApp.Infrastructure;
using HVTApp.Modules.BookRegistration.ViewModels;
using HVTApp.UI.Tabs;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.Modules.BookRegistration.Views
{
    [RibbonTab(typeof(TabCRUD))]
    public partial class BookRegistrationView : ViewBase
    {
        private readonly BookRegistrationViewModel _viewModel;
        public BookRegistrationView(BookRegistrationViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();

            //назначаем контексты
            _viewModel = viewModel;
            this.DataContext = viewModel;

            this.Loaded += OnLoaded;
        }

        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            await _viewModel.LoadAsync();
            this.Loaded -= OnLoaded;
        }
    }
}
