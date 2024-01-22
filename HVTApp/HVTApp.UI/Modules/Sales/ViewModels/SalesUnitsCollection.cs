using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model.POCOs;
using Microsoft.Practices.ObjectBuilder2;

namespace HVTApp.UI.Modules.Sales.ViewModels
{
    public class SalesUnitsCollection : IList<SalesUnit>
    {
        private readonly List<SalesUnit> _salesUnits;

        public void Add(SalesUnit salesUnit)
        {
            if (salesUnit == null)
            {
                throw new ArgumentNullException(nameof(salesUnit));
            }

            //добавляем новый юнит
            if (_salesUnits.Any(unit => ReferenceEquals(unit, salesUnit)) == false)
            {
                _salesUnits.Add(salesUnit);
            }

            //удаляем старый юнит, если он есть
            _salesUnits
                .Where(unit => unit.Id == salesUnit.Id && !ReferenceEquals(unit, salesUnit))
                .ToList()
                .ForEach(unit => _salesUnits.Remove(unit));

            CollectionChanged?.Invoke();
        }

        public bool Remove(SalesUnit salesUnit)
        {
            var result = _salesUnits.RemoveById(salesUnit);

            if (result)
            {
                if (!_salesUnits.Any())
                {
                    CollectionIsEmptyEvent?.Invoke();
                }

                CollectionChanged?.Invoke();
            }

            return result;
        }

        public event Action CollectionChanged;

        public event Action CollectionIsEmptyEvent; 

        #region

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

        #endregion
    }
}