using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace HVTApp.Model.Wrappers
{
    public class UnitsGroup
    {
        public ObservableCollection<UnitWrapper> Units { get; }
        public string ProductName => Units.First().ProductionsUnit.Product.Designation;
        public int Count => Units.Count;

        public UnitsGroup(IEnumerable<UnitWrapper> units)
        {
            Units = new ObservableCollection<UnitWrapper>(units);
            //Units.CollectionChanged += SalesUnitsOnCollectionChanged;
        }

        private void SalesUnitsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            throw new NotImplementedException();
        }
    }
}
