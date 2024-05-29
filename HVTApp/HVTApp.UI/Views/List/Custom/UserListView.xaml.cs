using System.Windows;
using HVTApp.Infrastructure;
using HVTApp.UI.ViewModels;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Views
{
    public partial class UserListView : ViewBase
    {
        public UserListView()
        {
            InitializeComponent();
        }

        public UserListView(IRegionManager regionManager, IEventAggregator eventAggregator, UserLookupListViewModel UserLookupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = UserLookupListViewModel;
            UserLookupListViewModel.Loaded += () => { this.Loaded -= OnLoaded; };
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            ((UserLookupListViewModel)DataContext).Load();
        }
    }
}
