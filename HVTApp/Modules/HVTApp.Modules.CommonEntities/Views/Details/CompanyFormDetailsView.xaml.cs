using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Views
{
    public partial class CompanyFormDetailsView
    {
        public CompanyFormDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
        }
    }
}
