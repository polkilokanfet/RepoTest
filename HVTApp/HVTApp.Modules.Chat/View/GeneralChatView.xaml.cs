using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.Modules.Messenger.View
{
    //[RibbonTab(typeof(TabDirectumTask))]
    public partial class GeneralChatView : ViewBaseConfirmNavigationRequest
    {
        private readonly GeneralChatViewModel _viewModel;

        public GeneralChatView(IUnityContainer container, IRegionManager regionManager, IEventAggregator eventAggregator) : base(container, regionManager, eventAggregator)
        {
            _viewModel = ((Messenger)Container.Resolve<IMessenger>()).GeneralChatViewModel;
            InitializeComponent();
            this.DataContext = _viewModel;
        }

        public override bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        protected override bool IsSomethingChanged()
        {
            return false;
        }
    }
}
