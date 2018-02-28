using System.Collections.Generic;
using HVTApp.UI.Wrapper;

namespace HVTApp.UI.ViewModels
{
    public class OfferUnitsGrouped : UnitsGrouped<OfferUnitWrapper>
    {
        public OfferUnitsGrouped(IEnumerable<OfferUnitWrapper> unitWrappers) : base(unitWrappers)
        {
        }

        public virtual OfferWrapper Offer
        {
            get { return GetValue<OfferWrapper>(); }
            set { SetValue(value); }
        }

        public virtual int ProductionTerm
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public double Total => Cost * Amount;
    }
}