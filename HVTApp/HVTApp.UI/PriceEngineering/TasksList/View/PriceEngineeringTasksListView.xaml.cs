using System;
using System.Windows;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.UI.PriceEngineering.Tabs;
using HVTApp.UI.PriceEngineering.ViewModel;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.PriceEngineering.View
{
    [RibbonTab(typeof(TabPriceEngineeringTasks))]
    public partial class PriceEngineeringTasksListView : ViewBase
    {
        #region UserConstructorVisibility

        //public static readonly DependencyProperty UserConstructorVisibilityProperty = DependencyProperty.Register(
        //    "UserConstructorVisibility", typeof(Visibility), typeof(PriceEngineeringTasksListView), new PropertyMetadata(Visibility.Visible));

        //public Visibility UserConstructorVisibility
        //{
        //    get => (Visibility) GetValue(UserConstructorVisibilityProperty);
        //    set => SetValue(UserConstructorVisibilityProperty, value);
        //}

        public Visibility UserConstructorVisibility { get; } = Visibility.Visible;


        #endregion

        public PriceEngineeringTasksListView(IUnityContainer container, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            switch (GlobalAppProperties.User.RoleCurrent)
            {
                case Role.SalesManager:
                    this.DataContext = container.Resolve<PriceEngineeringTasksListViewModelSalesManager>();
                    break;
                case Role.Constructor:
                    this.DataContext = container.Resolve<PriceEngineeringTasksListViewModelConstructor>();
                    UserConstructorVisibility = Visibility.Collapsed;
                    break;
                case Role.DesignDepartmentHead:
                    this.DataContext = container.Resolve<PriceEngineeringTasksListViewModelDesignDepartmentHead>();
                    break;
            }

            InitializeComponent();
        }
    }
}
