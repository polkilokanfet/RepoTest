using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using HVTApp.Model.Wrapper;

namespace HVTApp.UI.Modules.Reports.ViewModels
{
    public class SalesUnitFakeDataGroup
    {
        private double? _cost;
        private DateTime? _realizationDate;
        private DateTime? _orderInTakeDate;
        public IValidatableChangeTrackingCollection<SalesUnitWrapper> SalesUnits { get; }

        public string Facility => SalesUnits.First().Model.Facility.ToString();
        public string Product => SalesUnits.First().Model.Product.ToString();
        public int Amount => SalesUnits.Count;

        public double? Cost
        {
            get { return _cost; }
            set
            {
                _cost = value;
                foreach (var salesUnitWrapper in SalesUnits)
                {
                    salesUnitWrapper.FakeData.Cost = value;
                }
            }
        }

        public DateTime? RealizationDate
        {
            get { return _realizationDate; }
            set
            {
                _realizationDate = value;
                foreach (var salesUnitWrapper in SalesUnits)
                {
                    salesUnitWrapper.FakeData.RealizationDate = value;
                }

            }
        }

        public DateTime? OrderInTakeDate
        {
            get { return _orderInTakeDate; }
            set
            {
                _orderInTakeDate = value;
                foreach (var salesUnitWrapper in SalesUnits)
                {
                    salesUnitWrapper.FakeData.OrderInTakeDate = value;
                }

            }
        }

        public PaymentConditionSet PaymentConditionSet { get; set; }

        public SalesUnitFakeDataGroup(IEnumerable<SalesUnit> salesUnits)
        {
            SalesUnits = new ValidatableChangeTrackingCollection<SalesUnitWrapper>(salesUnits.Select(x => new SalesUnitWrapper(x)));
            foreach (var salesUnitWrapper in SalesUnits)
            {
                if (salesUnitWrapper.FakeData == null)
                {
                    salesUnitWrapper.FakeData = new FakeDataWrapper(new FakeData());
                }
            }

            _cost = SalesUnits.First().FakeData.Cost;
            _orderInTakeDate = SalesUnits.First().FakeData.OrderInTakeDate;
            _realizationDate = SalesUnits.First().FakeData.RealizationDate;
        }
    }
}