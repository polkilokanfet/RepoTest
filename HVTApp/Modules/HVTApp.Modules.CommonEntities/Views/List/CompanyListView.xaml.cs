using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Views
{
    public partial class CompanyListView 
    {
        public CompanyListView(IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
        }
    }
}
