using System.Globalization;
using System.Windows;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Converters;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.TechnicalRequrementsTasksModule
{
    [RibbonTab(typeof(Tabs.TabTechnicalRequrementsTasksView))]
    public partial class TechnicalRequrementsTasksView
    {
        public TechnicalRequrementsTasksView(TechnicalRequrementsTasksViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            this.DataContext = viewModel;

            //отображение колонки FrontManager
//            FieldLayout1.Fields[3].Visibility = (Visibility)((new BooleanToVisibilityReverseConverter()).Convert(viewModel.CurrentUserIsManager, null, null, CultureInfo.CurrentCulture));
        }
    }
}
