using System;
using System.Collections.Specialized;
using Infragistics.Windows.OutlookBar;
using Prism.Regions;

namespace HVTApp.Infrastructure.Prism
{
    public class XamOutlookBarRegionAdapter : RegionAdapterBase<XamOutlookBar>
    {
        public XamOutlookBarRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory) : base(regionBehaviorFactory)
        {
        }

        protected override void Adapt(IRegion region, XamOutlookBar regionTarget)
        {
            if (region == null) throw new NullReferenceException("region is null");
            if (regionTarget == null) throw new NullReferenceException("regionTarget is null");

            region.ActiveViews.CollectionChanged += (sender, args) =>
            {
                switch (args.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                    {
                        foreach (OutlookBarGroup newGroup in args.NewItems)
                        {
                            regionTarget?.Groups.Add(newGroup);
                            
                            //выделение первой добавленной группы
                            if (Equals(regionTarget.Groups[0], newGroup))
                                regionTarget.SelectedGroup = newGroup;
                        }
                        break;
                    }
                    case NotifyCollectionChangedAction.Remove:
                    {
                        foreach (OutlookBarGroup oldGroup in args.OldItems)
                        {
                            regionTarget?.Groups.Remove(oldGroup);
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
    }
}
