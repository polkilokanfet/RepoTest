using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using Microsoft.Practices.ObjectBuilder2;

namespace HVTApp.UI.Modules.Sales.Project1
{
    public class PoductsIncludedCollection : IValidatableChangeTrackingCollection<ProductIncludedWrapper>
    {
        private readonly ProjectUnitGroup _projectUnitGroup;

        public PoductsIncludedCollection(ProjectUnitGroup projectUnitGroup)
        {
            _projectUnitGroup = projectUnitGroup;
        }

        public IEnumerator<ProductIncludedWrapper> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(ProductIncludedWrapper item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            _projectUnitGroup.ForEach(x => x.ProductsIncluded.Clear());
        }

        public bool Contains(ProductIncludedWrapper item)
        {
            return _projectUnitGroup.SelectMany(x => x.ProductsIncluded).Contains(item);
        }

        public void CopyTo(ProductIncludedWrapper[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(ProductIncludedWrapper item)
        {
            throw new NotImplementedException();
        }

        public int Count => _projectUnitGroup.SelectMany(x => x.ProductsIncluded).Count();

        public bool IsReadOnly => false;

        public int IndexOf(ProductIncludedWrapper item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, ProductIncludedWrapper item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public ProductIncludedWrapper this[int index]
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public void AcceptChanges()
        {
            _projectUnitGroup.ForEach(x => x.ProductsIncluded.AcceptChanges());
        }

        public bool IsChanged => _projectUnitGroup.Select(x => x.ProductsIncluded).Any(x => x.IsChanged);
        public void RejectChanges()
        {
            _projectUnitGroup.ForEach(x => x.ProductsIncluded.RejectChanges());
        }

        public bool IsValid => _projectUnitGroup.Select(x => x.ProductsIncluded).All(x => x.IsValid);

        public event PropertyChangedEventHandler PropertyChanged;
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public ReadOnlyObservableCollection<ProductIncludedWrapper> AddedItems { get; }
        public ReadOnlyObservableCollection<ProductIncludedWrapper> ModifiedItems { get; }
        public ReadOnlyObservableCollection<ProductIncludedWrapper> RemovedItems { get; }
    }
}