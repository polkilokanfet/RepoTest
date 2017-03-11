using System.Windows.Controls;
using HVTApp.Infrastructure;
using HVTApp.Modules.CommonEntities.Menus;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.Modules.CommonEntities.Views
{
    /// <summary>
    /// Interaction logic for CompanyFormsView
    /// </summary>
    [RibbonTab(typeof(TabCompaniesForms))]
    public partial class CompanyFormsView
    {
        public CompanyFormsView(IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
        }
    }
}
