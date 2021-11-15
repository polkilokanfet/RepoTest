using System;
using System.Windows;
using HVTApp.Infrastructure;
using HVTApp.Model.Services;
using HVTApp.UI.Modules.BookRegistration.Tabs;
using HVTApp.UI.Modules.BookRegistration.ViewModels;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.BookRegistration.Views
{
    [RibbonTab(typeof(TabBookRegistration))]
    public partial class BookRegistrationView
    {
        private readonly IFileManagerService _fileManagerService;
        private Uri _currentUri;

        public BookRegistrationView(BookRegistrationViewModel viewModel, IFileManagerService fileManagerService, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            _fileManagerService = fileManagerService;
            InitializeComponent();
            viewModel.Load2();
            DataContext = viewModel;

            viewModel.SelectedDocumentChanged += document=>
            {
                if (document!= null)
                {
                    _currentUri = new Uri(_fileManagerService.GetPath(document));
                    this.Browser.Source = _currentUri;
                }
            };
        }


        private void GoBackButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (this.Browser.CanGoBack && this.Browser.Source != _currentUri)
                Browser.GoBack();
        }

        private void GoForwardButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (this.Browser.CanGoForward)
                Browser.GoForward();
        }

    }
}
