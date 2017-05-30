using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace HVTApp.Model.Wrappers
{
    public class SalesUnitsGroup
    {
        public ObservableCollection<SalesUnitWrapper> SalesUnits { get; }
        public string ProductName => SalesUnits.First().ProductionUnit.Product.Designation;
        public int Count => SalesUnits.Count;

        public SalesUnitsGroup(IEnumerable<SalesUnitWrapper> salesUnits)
        {
            SalesUnits = new ObservableCollection<SalesUnitWrapper>(salesUnits);
            //SalesUnits.CollectionChanged += SalesUnitsOnCollectionChanged;
        }

        private void SalesUnitsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            throw new NotImplementedException();
        }
    }
}
