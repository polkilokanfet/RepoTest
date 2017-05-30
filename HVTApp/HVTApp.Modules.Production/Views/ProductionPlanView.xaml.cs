using Prism.Events;
using Prism.Regions;

namespace HVTApp.Modules.Production.Views
{
    public partial class ProductionPlanView
    {
        public ProductionPlanView(IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
        }
    }
}
