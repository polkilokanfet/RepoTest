using System.Windows.Controls;
using HVTApp.Infrastructure;
using HVTApp.Modules.CommonEntities.Menus;
using Prism.Events;
using Prism.Regions;
using TabCRUD = HVTApp.Modules.CommonEntities.Tabs.TabCRUD;

namespace HVTApp.Modules.CommonEntities.Views
{
    [RibbonTab(typeof(TabCRUD))]
    public partial class CompanyFormsView
    {
        public CompanyFormsView(IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
        }
    }
}
