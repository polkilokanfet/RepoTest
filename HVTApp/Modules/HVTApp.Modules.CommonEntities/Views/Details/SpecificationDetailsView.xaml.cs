using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Views
{
    public partial class SpecificationDetailsView
    {
        public SpecificationDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
        }
    }
}
