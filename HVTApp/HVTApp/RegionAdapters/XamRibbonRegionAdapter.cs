using System;
using System.Collections.Specialized;
using HVTApp.Infrastructure;
using Infragistics.Windows.Ribbon;
using Prism.Regions;

namespace HVTApp.RegionAdapters
{
    public class XamRibbonRegionAdapter : RegionAdapterBase<XamRibbon>
    {
        public XamRibbonRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory) : base(regionBehaviorFactory)
        {
        }

        protected override void Adapt(IRegion region, XamRibbon regionTarget)
        {
            if (region == null) throw new NullReferenceException("region is null");
            if (regionTarget == null) throw new NullReferenceException("regionTarget is null");

            region.ActiveViews.CollectionChanged += (sender, args) =>
            {
                switch (args.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        {
                            foreach (object view in args.NewItems)
                            {
                                AddViewToRegion(view, regionTarget);
                            }
                            break;
                        }
                    case NotifyCollectionChangedAction.Remove:
                        {
                            foreach (object view in args.OldItems)
                            {
                                RemoveViewToRegion(view, regionTarget);
                            }
                            break;
                        }
                }
            };
        }


        protected override IRegion CreateRegion()
        {
            return new AllActiveRegion();
        }

        private static void AddViewToRegion(object view, XamRibbon xamRibbon)
        {
            var ribbonTabItem = view as RibbonTabItemWithViewModel;
            if(ribbonTabItem != null)
                xamRibbon.Tabs.Add(ribbonTabItem);
        }
        private void RemoveViewToRegion(object view, XamRibbon xamRibbon)
        {
            var ribbonTabItem = view as RibbonTabItemWithViewModel;
            if (ribbonTabItem != null)
                xamRibbon.Tabs.Remove(ribbonTabItem);
        }
    }
}