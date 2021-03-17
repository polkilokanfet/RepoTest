using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Settings.Views
{
    public partial class UserSettingsView
    {
        public UserSettingsView(IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
        }
    }
}
