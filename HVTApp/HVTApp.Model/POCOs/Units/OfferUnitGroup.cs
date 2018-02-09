using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public partial class OfferUnitGroup : BaseEntity
    {
        public OfferUnitGroup(List<OfferUnit> offerUnits)
        {
            OfferUnits = offerUnits;
        }

        public virtual Offer Offer
        {
            get { return GetValue<Offer>(); }
            set { SetValue(value); }
        }

        public virtual Product Product
        {
            get { return GetValue<Product>(); }
            set { SetValue(value); }
        }

        public virtual Facility Facility
        {
            get { return GetValue<Facility>(); }
            set { SetValue(value); }
        }

        public virtual int ProductionTerm
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public double Cost
        {
            get { return GetValue<double>(); }
            set { SetValue(value); }
        }

        public int Amount => OfferUnits.Count;

        public virtual List<OfferUnit> OfferUnits { get; set; } = new List<OfferUnit>();

        private T GetValue<T>([CallerMemberName] string propertyName = null)
        {
            var unit = OfferUnits.First();
            return (T)unit.GetType().GetProperty(propertyName).GetValue(unit);
        }

        private void SetValue(object value, [CallerMemberName] string propertyName = null)
        {
            var unit = OfferUnits.First();
            var propertyInfo = unit.GetType().GetProperty(propertyName);
            foreach (var offerUnit in OfferUnits)
                propertyInfo.SetValue(offerUnit, value);
        }

    }
}