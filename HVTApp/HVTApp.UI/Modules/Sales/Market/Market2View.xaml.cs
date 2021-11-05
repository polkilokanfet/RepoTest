using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Services;
using HVTApp.UI.Modules.Sales.Market.Tabs;
using Infragistics.Windows.DataPresenter;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Sales.Market
{
    [RibbonTab(typeof(MarketMainTab))]
    [RibbonTab(typeof(MarketViewTab))]
    [RibbonTab(typeof(MarketSettingsTab))]
    public partial class Market2View
    {
        private readonly Market2ViewModel _viewModel;
        private readonly IMessagesOutlookService _messagesOutlookService;
        private Uri _currentUri;

        protected override XamDataGrid DataGrid => this.ContentControl.Content as XamDataGrid;

        public Market2View(Market2ViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator, IMessageService messageService, IMessagesOutlookService messagesOutlookService) : base(viewModel, regionManager, eventAggregator, messageService)
        {
            _viewModel = viewModel;
            _messagesOutlookService = messagesOutlookService;
            InitializeComponent();

            //приложения проекта
            viewModel.SelectedProjectItemChanged += projectItem =>
            {
                if (projectItem != null)
                {
                    _currentUri = new Uri(PathGetter.GetPath(projectItem.Project));
                    this.Browser.Source = _currentUri;
                }
            };

            this.Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (_viewModel.IsLoaded)
            {
                ExpandCollapseMethod(true);
                ExpandCollapseMethod(false);
            }

            this.Loaded -= OnLoaded;
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

        /// <summary>
        /// Перетаскивание сообщений
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Messages_OnDrop(object sender, DragEventArgs e)
        {
            //если нет выделенного проекта - некуда копировать
            if (_viewModel.SelectedProjectItem == null)
                return;

            //если то, что перетащили - файлы
            if (e.Data.GetData(DataFormats.FileDrop) is string[] paths)
            {
                //отбираем только .msg
                paths = paths.Where(path => Path.GetExtension(path) == ".msg").ToArray();

                if (paths.Any())
                {
                    //куда будем копировать
                    var correspondencePath = PathGetter.GetPath(_viewModel.SelectedProjectItem.Project);
                    correspondencePath = Path.Combine(correspondencePath, PathGetter.CorrespondenceFolderName);

                    foreach (var path in paths)
                    {
                        try
                        {
                            MessageOutlook messageOutlook = _messagesOutlookService.GetOutlookMessage(path);

                            //если такое сообщение уже есть - пропускаем
                            if (_viewModel.Outlook.Messages.Any(x => x.Equals(messageOutlook)))
                            {
                                continue;
                            }

                            //копируем сообщение
                            File.Copy(path, Path.Combine(correspondencePath, $"{Guid.NewGuid()}.msg"));
                            _viewModel.Outlook.Messages.Add(messageOutlook);
                        }
                        catch
                        {
                            // ignored
                        }
                    }
                }
            }

        }
    }
}
