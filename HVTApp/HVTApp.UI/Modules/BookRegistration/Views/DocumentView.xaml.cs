using System.Linq;
using System.Windows;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.UI.Modules.BookRegistration.Tabs;
using HVTApp.UI.Modules.BookRegistration.ViewModels;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.BookRegistration.Views
{
    [RibbonTab(typeof(TabDocument))]
    public partial class DocumentView
    {
        private readonly DocumentViewModel _viewModel;

        public DocumentView(DocumentViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            _viewModel = viewModel;
            InitializeComponent();
            this.DataContext = viewModel;
        }

        public override bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            var parameter = navigationContext.Parameters.First();

            if (Equals(parameter.Key, DocumentDirection.Outgoing.ToString()))
            {
                this.DocumentDetailsView.VisibilityAuthorDocument = Visibility.Collapsed;
            }

            _viewModel.Load2(parameter.Value as Document);
            base.OnNavigatedTo(navigationContext);
        }
    }
}
