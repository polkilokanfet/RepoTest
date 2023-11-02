using System;
using System.IO;
using System.Linq;
using System.Windows;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.UI.Modules.BookRegistration.Tabs;
using HVTApp.UI.Modules.BookRegistration.ViewModels;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.BookRegistration.Views
{
    [RibbonTab(typeof(TabDocument))]
    public partial class DocumentView : ViewBaseConfirmNavigationRequest
    {
        private readonly IFileManagerService _fileManagerService;
        private readonly DocumentViewModel _viewModel;

        public DocumentView(DocumentViewModel viewModel, IUnityContainer container, IRegionManager regionManager, IEventAggregator eventAggregator) : base(container, regionManager, eventAggregator)
        {
            _viewModel = viewModel;
            _fileManagerService = container.Resolve<IFileManagerService>();
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

            //если документ исходящий
            if (Equals(parameter.Key, DocumentDirection.Outgoing.ToString()))
            {
                this.DocumentDetailsView.VisibilityAuthorDocument = Visibility.Collapsed;
                this.DocumentDetailsView.VisibilityTceNumberDocument = Visibility.Collapsed;
            }

            //если создаем документ
            if (navigationContext.Parameters.Count() == 1)
            {
                _viewModel.LoadCreate(parameter.Value as Document, parameter.Key);
            }
            //если редактируем документ
            else
            {
                _viewModel.LoadEdit(parameter.Value as Document);
            }

            this.Browser.Source = new Uri(_fileManagerService.GetPath(_viewModel.Item.Model));

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

        protected override bool IsSomethingChanged()
        {
            return _viewModel.Item.IsChanged;
        }

        public override void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            //если не добавлено вложений
            if (!Directory.EnumerateFileSystemEntries(_fileManagerService.GetPath(_viewModel.Item.Model)).Any())
            {
                var dr = Container.Resolve<IMessageService>().ConfirmationDialog("Внимание!", "Вы не добавили вложения. \nПродолжить не добавляя вложения?", defaultNo: true);
                if (dr == false)
                {
                    continuationCallback(false);
                    return;
                }
            }

            base.ConfirmNavigationRequest(navigationContext, continuationCallback);
        }
    }
}
