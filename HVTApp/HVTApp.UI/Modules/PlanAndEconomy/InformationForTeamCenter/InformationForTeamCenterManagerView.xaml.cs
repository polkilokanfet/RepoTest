using HVTApp.Infrastructure;
using HVTApp.UI.Modules.Reports.CommonInfo;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.PlanAndEconomy.InformationForTeamCenter
{
    [RibbonTab(typeof(TabCommonInfo))]
    public partial class InformationForTeamCenterManagerView
    {
        public InformationForTeamCenterManagerView(CommonInfoViewModel commonInfoViewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();

            this.DataContext = commonInfoViewModel;
            this.CommonInfoView.DataContext = commonInfoViewModel;
        }
    }
}
