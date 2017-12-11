using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Views
{
    public partial class CompanyDetailsView
    {
        public CompanyDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
        }
    }
}
