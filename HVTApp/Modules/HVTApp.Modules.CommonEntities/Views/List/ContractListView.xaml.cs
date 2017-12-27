using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Views
{
    public partial class ContractListView 
    {
        public ContractListView(IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
        }
    }
}
