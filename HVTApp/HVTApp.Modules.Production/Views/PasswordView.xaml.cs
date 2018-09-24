using Prism.Events;
using Prism.Regions;

namespace HVTApp.Modules.Settings.Views
{
    public partial class PasswordView
    {
        public PasswordView(IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
        }
    }
}
