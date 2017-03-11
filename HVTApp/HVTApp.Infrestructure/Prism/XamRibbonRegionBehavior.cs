using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Regions;
using System.Reflection;

namespace HVTApp.Infrastructure.Prism
{
    public class XamRibbonRegionBehavior : RegionBehavior
    {
        /// <summary>
        /// The key of this behavior.
        /// </summary>
        public const string BehaviorKey = "XamRibbonRegionBehavior";

        protected override void OnAttach()
        {
            if(Region.Name == RegionNames.ContentRegion)
                Region.ActiveViews.CollectionChanged += ActiveViewsOnCollectionChanged;
        }

        private void ActiveViewsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                bool isFirst = true;
                foreach (var newView in e.NewItems)
                {
                    IViewBase view = newView as IViewBase;
                    if (view == null) continue; //имеем ли дело с нужным типом?

                    if(view.RibbonTabs.Count > 0) continue; //возможно список уже сформирован

                    foreach (RibbonTabAttribute ribbonTabAttribute in GetCustomAttributes<RibbonTabAttribute>(newView.GetType()))
                    {
                        IRibbonTabItem ribbonTabItem = (IRibbonTabItem)Activator.CreateInstance(ribbonTabAttribute.RibbonTabType);
                        ribbonTabItem.ViewModel = view.ViewModel;

                        view.RibbonTabs.Add(ribbonTabItem);
                        ribbonTabItem.IsSelected = isFirst;

                        if (isFirst) isFirst = false;
                    }

                }
            }
        }

        private IEnumerable<T> GetCustomAttributes<T>(Type type)
        {
            return type.GetCustomAttributes(typeof(T), true).OfType<T>();
        }
    }
}
