using System.Windows.Controls;
using HVTApp.Infrastructure;
using HVTApp.Modules.CommonEntities.Menus;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.Modules.CommonEntities.Views
{
    /// <summary>
    /// Interaction logic for CompaniesView
    /// </summary>
    [RibbonTab(typeof(TabCompanies))]
    public partial class CompaniesView 
    {
        public CompaniesView(IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
        }
    }
}
