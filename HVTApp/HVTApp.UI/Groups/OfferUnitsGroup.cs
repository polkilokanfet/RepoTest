using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.UI.Wrapper;

namespace HVTApp.UI.Groups
{
    public class OfferUnitsGroup : 
        BaseWrappersGroup<OfferUnitsGroup, OfferUnit, OfferUnitWrapper>, 
        IGroupValidatableChangeTrackingWithCollection<OfferUnitsGroup, OfferUnit>
    {
        /// <summary>
        /// ������� ������ � ���
        /// </summary>
        public int Position { get; set; }

        /// <summary>
        /// ���
        /// </summary>
        public OfferWrapper Offer
        {
            get { return GetValue<OfferWrapper>(); }
            set { SetValue(value); }
        }

        public OfferUnitsGroup(IEnumerable<OfferUnit> units) : base(units.ToList())
        {
        }
    }
}