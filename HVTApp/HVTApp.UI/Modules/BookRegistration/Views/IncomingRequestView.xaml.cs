using System;
using System.Linq;
using System.Windows;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.UI.Modules.BookRegistration.Tabs;
using HVTApp.UI.Modules.BookRegistration.ViewModels;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.BookRegistration.Views
{
    [RibbonTab(typeof(TabIncomingRequest))]
    public partial class IncomingRequestView
    {
        private readonly IFileManagerService _fileManagerService;

        private readonly IncomingRequestViewModel _viewModel;

        public IncomingRequestView(IncomingRequestViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator, IFileManagerService fileManagerService) : base(regionManager, eventAggregator)
        {
            _viewModel = viewModel;
            _fileManagerService = fileManagerService;
            InitializeComponent();
            DataContext = viewModel;
        }

        public override bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            var request = navigationContext.Parameters.Single(x => x.Value is IncomingRequest).Value as IncomingRequest;
            var unitOfWork = navigationContext.Parameters.Single(x => x.Value is IUnitOfWork).Value as IUnitOfWork;

            _viewModel.Load(request, unitOfWork);

            this.Browser.Source = new Uri(_fileManagerService.GetPath(_viewModel.Item.Model.Document));

            base.OnNavigatedTo(navigationContext);
        }


        private void GoBackButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (this.Browser.CanGoBack)
                Browser.GoBack();
        }

        private void GoForwardButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (this.Browser.CanGoForward)
                Browser.GoForward();
        }
    }
}
