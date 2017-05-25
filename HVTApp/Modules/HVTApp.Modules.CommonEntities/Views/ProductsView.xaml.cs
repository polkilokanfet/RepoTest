using System.Windows.Controls;
using HVTApp.Infrastructure;
using HVTApp.Modules.CommonEntities.Menus;
using Prism.Events;
using Prism.Regions;
using TabCRUD = HVTApp.Modules.CommonEntities.Tabs.TabCRUD;

namespace HVTApp.Modules.CommonEntities.Views
{
    /// <summary>
    /// Interaction logic for ProductsView
    /// </summary>
    [RibbonTab(typeof(TabCRUD))]
    public partial class ProductsView 
    {
        public ProductsView(IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
        }
    }
}
