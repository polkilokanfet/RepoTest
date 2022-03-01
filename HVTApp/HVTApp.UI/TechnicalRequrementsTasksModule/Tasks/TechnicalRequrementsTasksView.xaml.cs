using System.Globalization;
using System.Windows;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Converters;
using HVTApp.Model;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.TechnicalRequrementsTasksModule
{
    [RibbonTab(typeof(Tabs.TabTechnicalRequrementsTasksView))]
    public partial class TechnicalRequrementsTasksView
    {
        public TechnicalRequrementsTasksView(IUnityContainer container, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();

            if (GlobalAppProperties.User.RoleCurrent == Role.SalesManager)
            {
                this.DataContext = container.Resolve<TechnicalRequrementsTasksForFrontManagerViewModel>();
            }
            else if (GlobalAppProperties.User.RoleCurrent == Role.BackManager)
            {
                this.DataContext = container.Resolve<TechnicalRequrementsTasksForBackManagerViewModel>();
            }
            else if (GlobalAppProperties.User.RoleCurrent == Role.BackManagerBoss)
            {
                this.DataContext = container.Resolve<TechnicalRequrementsTasksForBackManagerBossViewModel>();
            }

            //отображение колонки FrontManager
            //            FieldLayout1.Fields[3].Visibility = (Visibility)((new BooleanToVisibilityReverseConverter()).Convert(viewModel.CurrentUserIsManager, null, null, CultureInfo.CurrentCulture));
        }
    }
}
