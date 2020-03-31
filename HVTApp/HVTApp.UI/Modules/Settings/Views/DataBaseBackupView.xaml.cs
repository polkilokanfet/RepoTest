using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Settings.Views
{
    public partial class DataBaseBackupView
    {
        public DataBaseBackupView(IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
        }
    }
}
