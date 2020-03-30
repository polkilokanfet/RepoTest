using System;
using System.Linq;
using System.Windows;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.Lookup;
using HVTApp.UI.Modules.BookRegistration.Tabs;
using HVTApp.UI.Modules.BookRegistration.ViewModels;
using Infragistics.Windows.Controls;
using Infragistics.Windows.DataPresenter;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.BookRegistration.Views
{
    [RibbonTab(typeof(TabIncomingRequests))]
    public partial class IncomingRequestsView
    {
        private Uri _currentUri;

        public IncomingRequestsView(IncomingRequestsViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = viewModel;
            if (GlobalAppProperties.User.RoleCurrent == Role.Admin || GlobalAppProperties.User.RoleCurrent == Role.Director)
            {
                var recordFilter = new RecordFilter { FieldName = nameof(IncomingRequestLookup.HasAnyPerformer) };
                var condition = new ComparisonCondition(ComparisonOperator.Equals, false);
                recordFilter.Conditions.Add(condition);
                IncomingRequestsGrid.FieldLayouts.First().RecordFilters.Add(recordFilter);

                var recordFilter2 = new RecordFilter { FieldName = nameof(IncomingRequest.IsActual) };
                var condition2 = new ComparisonCondition(ComparisonOperator.Equals, true);
                recordFilter2.Conditions.Add(condition2);
                IncomingRequestsGrid.FieldLayouts.First().RecordFilters.Add(recordFilter2);
            }

            if (GlobalAppProperties.User.RoleCurrent == Role.SalesManager)
            {
                var recordFilter = new RecordFilter { FieldName = nameof(IncomingRequest.IsDone) };
                var condition = new ComparisonCondition(ComparisonOperator.Equals, false);
                recordFilter.Conditions.Add(condition);
                IncomingRequestsGrid.FieldLayouts.First().RecordFilters.Add(recordFilter);

                //скрываем колонки
                IncomingRequestsGrid.FieldLayouts.First().Fields
                    .Single(x => x.Name == nameof(IncomingRequestLookup.HasAnyPerformer)).Visibility = Visibility.Collapsed;
            }

            viewModel.SelectedIncomingRequestChanged += request =>
            {
                if (request != null)
                {
                    _currentUri = new Uri(PathGetter.GetPath(request.Document));
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
