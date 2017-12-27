using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Views
{
    public partial class CompanyFormListView
    {
        public CompanyFormListView(IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
        }
    }
}
