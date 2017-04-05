using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace HVTApp.Model.Wrapper
{
    public interface IValidatableChangeTrackingCollection<TCollectionItem> : IList<TCollectionItem>, IValidatableChangeTracking, INotifyCollectionChanged
        where TCollectionItem : class, IValidatableChangeTracking
    {
        ReadOnlyObservableCollection<TCollectionItem> AddedItems { get; }
        ReadOnlyObservableCollection<TCollectionItem> ModifiedItems { get; }
        ReadOnlyObservableCollection<TCollectionItem> RemovedItems { get; }
    }
}