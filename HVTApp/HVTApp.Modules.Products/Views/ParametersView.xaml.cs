using Prism.Events;
using Prism.Regions;

namespace HVTApp.Modules.Products.Views
{
    public partial class ParametersView
    {
        public ParametersView(IRegionManager regionManager, IEventAggregator eventAggregator) : base( regionManager, eventAggregator)
        {
            InitializeComponent();
        }
    }
}
