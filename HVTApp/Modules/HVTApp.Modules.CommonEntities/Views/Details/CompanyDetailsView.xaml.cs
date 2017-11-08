using System.Windows.Controls;
using HVTApp.Infrastructure;
using Prism.Events;
using Prism.Regions;
using ViewBase = HVTApp.Infrastructure.ViewBase;

namespace HVTApp.UI.Views
{
    public partial class CompanyDetailsView : ViewBase
    {
        public CompanyDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
        }
    }
}
