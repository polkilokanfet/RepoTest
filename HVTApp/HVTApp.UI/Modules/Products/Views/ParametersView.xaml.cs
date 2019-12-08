using HVTApp.Infrastructure;
using HVTApp.UI.Modules.Products.Tabs;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Products.Views
{
    [RibbonTab(typeof(TabParameters))]
    public partial class ParametersView
    {
        public ParametersView(IRegionManager regionManager, IEventAggregator eventAggregator) : base( regionManager, eventAggregator)
        {
            InitializeComponent();
        }
    }
}
