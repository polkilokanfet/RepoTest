using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Modules.Sales.ViewModels
{
    public class SalesUnitsCollection : IEnumerable<SalesUnit>
    {
        private readonly List<SalesUnit> _salesUnits;

        /// <summary>
        /// Добавляет новый SalesUnit и удаляет старый, если он есть
        /// </summary>
        /// <param name="salesUnit"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void ReAdd(SalesUnit salesUnit)
        {
            if (salesUnit == null)
                throw new ArgumentNullException(nameof(salesUnit));

            //добавляем новый юнит
            if (_salesUnits.Any(unit => ReferenceEquals(unit, salesUnit)) == false) _salesUnits.Add(salesUnit);

            //удаляем старый юнит, если он есть
            _salesUnits
                .Where(unit => unit.Id == salesUnit.Id && ReferenceEquals(unit, salesUnit) == false)
                .ForEach(unit => _salesUnits.Remove(unit));

            CollectionChanged?.Invoke();
        }

        public void ReAdd(IEnumerable<SalesUnit> salesUnits)
        {
            foreach (var salesUnit in salesUnits)
            {
                this.ReAdd(salesUnit);
            }
        }

        public bool Remove(SalesUnit salesUnit)
        {
            var result = _salesUnits.RemoveById(salesUnit);

            if (result == true)
            {
                if (_salesUnits.Any() == false)
                {
                    CollectionIsEmptyEvent?.Invoke();
                }

                CollectionChanged?.Invoke();
            }

            return result;
        }

        public event Action CollectionChanged;

        public event Action CollectionIsEmptyEvent; 

        public SalesUnitsCollection(IEnumerable<SalesUnit> salesUnits)
        {
            _salesUnits = new List<SalesUnit>(salesUnits);
        }

        public bool AllSalesUnitsSameById(IEnumerable<SalesUnit> salesUnits)
        {
            var units = salesUnits as SalesUnit[] ?? salesUnits.ToArray();
            return units.AllContainsInById(_salesUnits) && 
                   units.Length == _salesUnits.Count;
        }

        public IEnumerable<SalesUnit> ContainsById(IEnumerable<SalesUnit> salesUnits)
        {
            return salesUnits.Where(salesUnit => this._salesUnits.ContainsById(salesUnit));
        }

        #region IEnumerable

        public IEnumerator<SalesUnit> GetEnumerator()
        {
            return _salesUnits.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}