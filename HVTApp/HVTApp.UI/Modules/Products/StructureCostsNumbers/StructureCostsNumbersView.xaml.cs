using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Products.StructureCostsNumbers
{
    public partial class StructureCostsNumbersView
    {
        public StructureCostsNumbersView(StructureCostsNumbersViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
