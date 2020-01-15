using System.Windows;
using HVTApp.Infrastructure;
using HVTApp.UI.Modules.BookRegistration.Tabs;
using HVTApp.UI.Modules.BookRegistration.ViewModels;
using HVTApp.UI.Tabs;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.BookRegistration.Views
{
    [RibbonTab(typeof(TabBookRegistration))]
    [RibbonTab(typeof(TabCRUD))]
    public partial class BookRegistrationView
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

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            _viewModel.Load();
            this.Loaded -= OnLoaded;
        }
    }
}
