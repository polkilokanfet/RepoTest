using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Modules.Sales.ViewModels
{
    public class SalesUnitsCollection : IList<SalesUnit>
    {
        private readonly List<SalesUnit> _salesUnits;

        public SalesUnitsCollection(IEnumerable<SalesUnit> salesUnits)
        {
            _salesUnits = new List<SalesUnit>(salesUnits);
        }

        public IEnumerator<SalesUnit> GetEnumerator()
        {
            return _salesUnits.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(SalesUnit salesUnit)
        {
            if (_salesUnits.Contains(salesUnit))
                return;

            //���������� ����
            var oldSalesUnit = _salesUnits.SingleOrDefault(x => x.Id == salesUnit.Id);

            _salesUnits.Add(salesUnit);

            //������� ���������� ����
            if (oldSalesUnit != null)
                _salesUnits.Remove(oldSalesUnit);

            CollectionChanged?.Invoke();
        }

        public void Clear()
        {
            _salesUnits.Clear();
        }

        public bool Contains(SalesUnit item)
        {
            return _salesUnits.Contains(item);
        }

        public void CopyTo(SalesUnit[] array, int arrayIndex)
        {
            _salesUnits.CopyTo(array, arrayIndex);
        }

        public bool Remove(SalesUnit salesUnit)
        {
            var result = _salesUnits.Remove(salesUnit);
            CollectionChanged?.Invoke();
            return result;
        }

        public int Count => _salesUnits.Count;
        public bool IsReadOnly => false;
        public int IndexOf(SalesUnit salesUnit)
        {
            return _salesUnits.IndexOf(salesUnit);
        }

        public void Insert(int index, SalesUnit salesUnit)
        {
            _salesUnits.Insert(index, salesUnit);
        }

        public void RemoveAt(int index)
        {
            _salesUnits.RemoveAt(index);
        }

        public SalesUnit this[int index]
        {
            get { return _salesUnits[index]; }
            set { _salesUnits[index] = value; }
        }

        public event Action CollectionChanged;
    }
}