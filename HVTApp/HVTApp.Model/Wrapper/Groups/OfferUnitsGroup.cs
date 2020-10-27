using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.Model.Wrapper.Groups
{
    public class OfferUnitsGroup : 
        BaseWrappersGroup<OfferUnitsGroup, OfferUnit, OfferUnit2Wrapper>, 
        IGroupValidatableChangeTrackingWithCollection<OfferUnitsGroup, OfferUnit>
    {
        /// <summary>
        /// Позиция строки в ТКП
        /// </summary>
        public int Position { get; set; }

        /// <summary>
        /// ТКП
        /// </summary>
        public OfferWrapper Offer
        {
            get { return GetValue<OfferWrapper>(); }
            set { SetValue(value); }
        }

        public OfferUnitsGroup(IEnumerable<OfferUnit> units) : base(units.ToList())
        {
        }

        protected override SalesUnit GetSalesUnit()
        {
            return null;
        }
    }
}