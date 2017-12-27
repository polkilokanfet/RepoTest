using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Views
{
    public partial class ProjectDetailsView
    {
        public ProjectDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
        }
    }
}
