using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;

namespace HVTApp.Infrastructure
{
    public class ViewBase : UserControl, IViewBase, INavigationAware
    {
        public IRegionManager RegionManager { get; }
        public IEventAggregator EventAggregator { get; }
        public IList<IRibbonTabItem> RibbonTabs { get; }


        public BindableBase ViewModel
        {
            get { return (BindableBase)DataContext; }
            set { DataContext = value; }
        }

        public ViewBase(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            RegionManager = regionManager;
            EventAggregator = eventAggregator;

            RibbonTabs = new List<IRibbonTabItem>();
        }

        #region INavigationAware
        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {
            //добавляем RibbonTabs
            if (RegionManager != null)
            {
                IRegion tabRegion = RegionManager.Regions[RegionNames.RibbonTabRegion];
                RibbonTabs.ToList().ForEach(tab => tabRegion.Add(tab));
            }
        }

        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {
            //удаляем RibbonTabs
            if (RegionManager != null)
            {
                IRegion tabRegion = RegionManager.Regions[RegionNames.RibbonTabRegion];
                RibbonTabs.ToList().ForEach(tab => tabRegion.Remove(tab));
            }
        }

        #endregion
    }
}
