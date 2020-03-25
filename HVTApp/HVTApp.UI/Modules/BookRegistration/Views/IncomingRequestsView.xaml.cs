using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model;
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
        public IncomingRequestsView(IncomingRequestsViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = viewModel;

            if (GlobalAppProperties.User.RoleCurrent == Role.Admin || GlobalAppProperties.User.RoleCurrent == Role.Director)
            {
                var recordFilter = new RecordFilter
                {
                    FieldName = "HasAnyPerformer"
                };
                var condition = new ComparisonCondition(ComparisonOperator.Equals, false);
                recordFilter.Conditions.Add(condition);
                IncomingRequestsGrid.FieldLayouts.First().RecordFilters.Add(recordFilter);
            }

            if (GlobalAppProperties.User.RoleCurrent == Role.SalesManager)
            {
                var recordFilter = new RecordFilter
                {
                    FieldName = "IsDone"
                };
                var condition = new ComparisonCondition(ComparisonOperator.Equals, false);
                recordFilter.Conditions.Add(condition);
                IncomingRequestsGrid.FieldLayouts.First().RecordFilters.Add(recordFilter);
            }
        }
    }
}
