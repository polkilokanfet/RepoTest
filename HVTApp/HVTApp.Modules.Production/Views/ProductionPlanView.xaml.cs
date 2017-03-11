using Prism.Events;
using Prism.Regions;

namespace HVTApp.Modules.Production.Views
{
    /// <summary>
    /// Interaction logic for ProductionPlanView
    /// </summary>
    public partial class ProductionPlanView
    {
        public ProductionPlanView(IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
        }
    }
}
