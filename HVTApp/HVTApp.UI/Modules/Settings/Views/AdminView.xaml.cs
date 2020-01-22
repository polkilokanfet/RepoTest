using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Settings.Views
{
    public partial class AdminView
    {
        public AdminView(IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
        }
    }
}
