using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Views
{
    public partial class ContractDetailsView 
    {
        public ContractDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
        }
    }
}
