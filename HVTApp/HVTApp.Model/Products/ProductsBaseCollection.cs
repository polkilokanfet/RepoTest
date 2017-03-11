using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HVTApp.Model
{
    public class ProductsBaseCollection<TProduct> : ICollection<TProduct>
        where TProduct : ProductBase
    {
        private readonly List<TProduct> _products = new List<TProduct>();
        public void Add(TProduct item)
        {
            if(_products.Any() && !_products.First().Equipment.Equals(item.Equipment))
                throw new ArgumentException("Техника всех членов коллекции должна совпадать");

            _products.Add(item);
        }

        public void Clear()
        {
            _products.Clear();
        }

        public bool Contains(TProduct item)
        {
            return _products.Contains(item);
        }

        public void CopyTo(TProduct[] array, int arrayIndex)
        {
            _products.CopyTo(array, arrayIndex);
        }

        public bool Remove(TProduct item)
        {
            return _products.Remove(item);
        }

        public int Count => _products.Count;
        public bool IsReadOnly => false;

        public IEnumerator<TProduct> GetEnumerator()
        {
            return _products.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) _products).GetEnumerator();
        }

    }
}