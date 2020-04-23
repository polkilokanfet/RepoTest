using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrapper.Base.TrackingCollections
{
    public interface IValidatableChangeTrackingCollection<TCollectionItem> : IList<TCollectionItem>, IValidatableChangeTracking, INotifyCollectionChanged
        where TCollectionItem : IValidatableChangeTracking
    {
        ReadOnlyObservableCollection<TCollectionItem> AddedItems { get; }
        ReadOnlyObservableCollection<TCollectionItem> ModifiedItems { get; }
        ReadOnlyObservableCollection<TCollectionItem> RemovedItems { get; }
    }
}